using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace FDP.OrderService.Data.Utilities
{
    public static class EntityValidationErrorsExtensions
    {
        public static string Stringify(this IEnumerable<DbEntityValidationResult> entityValidationErrors)
        {
            StringBuilder sBuilder = new StringBuilder();
            int entityCount = entityValidationErrors.Count();
            int entityCounter = 0;

            sBuilder.Append("Validation failed for one or more entities." + System.Environment.NewLine);

            foreach (DbEntityValidationResult dbEntityValidationResult in entityValidationErrors)
            {
                int validationErrorsCount = dbEntityValidationResult.ValidationErrors.Count;
                int validationErrorsCounter = 0;

                entityCounter++;

                sBuilder.AppendFormat("- Entity {0}{1}", dbEntityValidationResult.Entry.Entity.ToString(), System.Environment.NewLine);

                foreach (var item in dbEntityValidationResult.ValidationErrors)
                {
                    validationErrorsCounter++;

                    sBuilder.AppendFormat("    * {0}. {1}{2}",
                        item.PropertyName,
                        item.ErrorMessage,
                        validationErrorsCounter != validationErrorsCount ? System.Environment.NewLine : "");
                }

                // skip NewLine if is last key
                if (entityCounter != entityCount)
                {
                    sBuilder.Append(System.Environment.NewLine);
                }
            }

            return sBuilder.ToString();
        }
    }
}