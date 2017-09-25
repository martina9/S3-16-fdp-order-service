using Castle.Core;
using Castle.Facilities.Startable;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer; 
using Topshelf;
using Topshelf.Logging;

namespace FDP.OrderService.Infrastructure
{
    public class CreateRequestWindowsService : ServiceControl
    {
        private IWindsorContainer _container;
        private readonly LogWriter _logger = HostLogger.Get<CreateRequestWindowsService>(); 
        public bool Start(HostControl hostControl)
        {
            _container = new WindsorContainer();
            _container.Register(Component.For<IWindsorContainer>().Instance(_container));
            _container.AddFacility<StartableFacility>(f => f.DeferredTryStart());

            _container.Install(FromAssembly.Named("FDP.Infrastructure"));
            //Start comsuming messages only after everything else is registed and started
            _container.Register(Classes.FromAssemblyNamed("FDP.Subscribers")
                .BasedOn<IStartable>());
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _container.Dispose(); 
            _logger.Info("Stopped!");
            return true;
        }
    }
}