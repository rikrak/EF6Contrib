namespace PluralizationServices
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Infrastructure.Pluralization;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CRMContext())
            {
                var result = context.Cliente.ToList();
            }
        }
    }

    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class Producto
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }

    public class CRMContext
        :DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<Producto> Productos { get; set; }
    }

    public class Configuration
        :DbConfiguration
    {
        public Configuration()
        {
           
            SetPluralizationService(new SpanishPluralizationService());
        }
    }
}
