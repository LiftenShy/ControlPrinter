using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using CP.DependencyResolver;
using CP.Security;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using InjectionProperty = Microsoft.Practices.Unity.InjectionProperty;

namespace CP.Web.App_Start
{
    public class UnityConfig : DependencyResolverConfiguration
    {
        /// <summary>
        /// Stores flag, whether Initialize method is already invoked or not.
        /// </summary>
        private static Boolean _isInitialized = false;

        /// <summary>
        /// Sunchronisation routine object.
        /// </summary>
        private static readonly Object _syncObject = new Object();

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static DependencyResolverConfiguration Instance { get; private set; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Initialize()
        {
            lock (_syncObject)
            {
                if (!_isInitialized)
                {
                    _isInitialized = true;
                    Instance = new UnityConfig();
                    System.Web.Mvc.DependencyResolver.SetResolver(new UnityDependencyResolver(Instance.Container));
                    FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
                    FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(Instance.Container));
                }
            }
        }

        protected override void RegisterTypes()
        {
            base.RegisterTypes();
            this.Container.RegisterType<MembershipProvider, CustomMembershipProvider>(
                new InjectionProperty("UserService"));

            this.Container.RegisterType<RoleProvider, CustomRoleProvider>(
                new InjectionProperty("RoleService"));

            this.Container.BuildUp(Membership.Provider);
            this.Container.BuildUp(Roles.Provider);
        }
    }
}