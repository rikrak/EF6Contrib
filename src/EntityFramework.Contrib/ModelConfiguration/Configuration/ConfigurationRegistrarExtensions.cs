namespace System.Data.Entity.ModelConfiguration
{
    using System.Data.Entity.ModelConfiguration.Configuration;
    using System.Reflection;

    /// <summary>
    /// A collection of ConfigurationRegistrar extensions
    /// </summary>
    public static class ConfigurationRegistrarExtensions
    {
        /// <summary>
        /// Discovers all types that inherit from <see cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{TEntityType}" /> or
        /// <see cref="System.Data.Entity.ModelConfiguration.ComplexTypeConfiguration{TComplexType}" /> in THIS assembly and adds an instance
        /// of each discovered type to this registrar.
        /// </summary>
        /// <remarks>
        /// Note that only types that are abstract or generic type definitions are skipped. Every
        /// type that is discovered and added must provide a parameterless constructor.
        /// </remarks>
        /// <returns>The same ConfigurationRegistrar instance so that multiple calls can be chained.</returns>
        public static ConfigurationRegistrar AddFromThisAssembly(this ConfigurationRegistrar configurationRegistrar)
        {
            configurationRegistrar.AddFromAssembly(Assembly.GetCallingAssembly());

            return configurationRegistrar;
        }

        /// <summary>
        /// Discovers all types that inherit from <see cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{TEntityType}" /> or
        /// <see cref="System.Data.Entity.ModelConfiguration.ComplexTypeConfiguration{TComplexType}" /> in named assembly, specified by <paramref name="assemblyString"/> and adds an instance
        /// of each discovered type to this registrar.
        /// </summary>
        /// <param name="assemblyString">
        /// The assembly with configuration to be loaded or scan.
        /// </param>
        /// <remarks>
        /// Note that only types that are abstract or generic type definitions are skipped. Every
        /// type that is discovered and added must provide a parameterless constructor.
        /// </remarks>
        /// <returns>The same ConfigurationRegistrar instance so that multiple calls can be chained.</returns>
        public static ConfigurationRegistrar AddFromNamedAssembly(this ConfigurationRegistrar configurationRegistrar,string assemblyString)
        {
            configurationRegistrar.AddFromAssembly(
                Assembly.Load(assemblyString));

            return configurationRegistrar;
        }


        /// <summary>
        /// Discovers all types that inherit from <see cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{TEntityType}" /> or
        /// <see cref="System.Data.Entity.ModelConfiguration.ComplexTypeConfiguration{TComplexType}" /> in  assembly that contain the {TType}
        /// of each discovered type to this registrar.
        /// </summary>
        /// <typeparam name="TType">The type of assembly to find in.</typeparam>
        /// <returns>The same ConfigurationRegistrar instance so that multiple calls can be chained.</returns>
        public static ConfigurationRegistrar AddFromAssemblyOf<TType>(this ConfigurationRegistrar configurationRegistrar)
            where TType : class
        {
            configurationRegistrar
                .AddFromAssembly(typeof(TType).Assembly);

            return configurationRegistrar;
        }
    }
}
