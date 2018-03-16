namespace ExtendedSqlServerMigrationsGenerator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            this.CreateView("TheAllCustomersViewName", "SELECT * FROM [dbo].[Customers]");
        }
        
        public override void Down()
        {
            this.DropTable("dbo.Customers");
            this.DropView("TheAllCustomersViewName");
        }
    }
}
