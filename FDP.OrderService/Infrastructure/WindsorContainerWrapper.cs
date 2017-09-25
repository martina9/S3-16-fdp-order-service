﻿using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EasyNetQ;
using IServiceProvider = EasyNetQ.IServiceProvider;

namespace FDP.OrderService.Infrastructure
{
    public class WindsorContainerWrapper : IContainer, IDisposable
    {
        private readonly IWindsorContainer windsorContainer;

        public WindsorContainerWrapper(IWindsorContainer windsorContainer)
        {
            this.windsorContainer = windsorContainer;
        }

        public TService Resolve<TService>() where TService : class
        {
            return windsorContainer.Resolve<TService>();
        }

        public IServiceRegister Register<TService>(System.Func<IServiceProvider, TService> serviceCreator)
            where TService : class
        {
            windsorContainer.Register(Component.For<TService>().UsingFactoryMethod(() => serviceCreator(this)).LifeStyle.Singleton);
            return this;
        }

        public IServiceRegister Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            windsorContainer.Register(Component.For<TService>().ImplementedBy<TImplementation>().LifeStyle.Singleton);
            return this;
        }

        public void Dispose()
        {
            windsorContainer.Dispose();
        }
    }
}