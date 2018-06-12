namespace Ouvidoria.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class idCurso : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Usuario", "idCurso", "dbo.Curso");
            DropIndex("dbo.Usuario", new[] { "idCurso" });
            AlterColumn("dbo.Usuario", "idCurso", c => c.Int());
            CreateIndex("dbo.Usuario", "idCurso");
            AddForeignKey("dbo.Usuario", "idCurso", "dbo.Curso", "id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "idCurso", "dbo.Curso");
            DropIndex("dbo.Usuario", new[] { "idCurso" });
            AlterColumn("dbo.Usuario", "idCurso", c => c.Int(nullable: false));
            CreateIndex("dbo.Usuario", "idCurso");
            AddForeignKey("dbo.Usuario", "idCurso", "dbo.Curso", "id", cascadeDelete: true);
        }
    }
}