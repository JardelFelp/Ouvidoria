namespace Ouvidoria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remodel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DepartamentoDepoimentoes", newName: "Feedback");
            RenameTable(name: "dbo.EventoTipo", newName: "TipoDepoimento");
            DropForeignKey("dbo.Evento", "idEventoTipo", "dbo.EventoTipo");
            DropForeignKey("dbo.Evento", "idUsuario", "dbo.Usuario");
            DropIndex("dbo.Evento", new[] { "idEventoTipo" });
            DropIndex("dbo.Evento", new[] { "idUsuario" });
            CreateTable(
                "dbo.Depoimento",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false, maxLength: 100),
                        Descricao = c.String(nullable: false, maxLength: 4000),
                        Resposta = c.String(maxLength: 4000),
                        Respondido = c.Boolean(nullable: false),
                        idTipoDepoimento = c.Int(nullable: false),
                        idUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.TipoDepoimento", t => t.idTipoDepoimento, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.idUsuario, cascadeDelete: true)
                .Index(t => t.idTipoDepoimento)
                .Index(t => t.idUsuario);
            
            DropTable("dbo.Evento");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Evento",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false, maxLength: 100),
                        Descricao = c.String(nullable: false, maxLength: 4000),
                        Resposta = c.String(maxLength: 4000),
                        Respondido = c.Boolean(nullable: false),
                        idEventoTipo = c.Int(nullable: false),
                        idUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            DropForeignKey("dbo.Depoimento", "idUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Depoimento", "idTipoDepoimento", "dbo.TipoDepoimento");
            DropIndex("dbo.Depoimento", new[] { "idUsuario" });
            DropIndex("dbo.Depoimento", new[] { "idTipoDepoimento" });
            DropTable("dbo.Depoimento");
            CreateIndex("dbo.Evento", "idUsuario");
            CreateIndex("dbo.Evento", "idEventoTipo");
            AddForeignKey("dbo.Evento", "idUsuario", "dbo.Usuario", "id", cascadeDelete: true);
            AddForeignKey("dbo.Evento", "idEventoTipo", "dbo.EventoTipo", "id", cascadeDelete: true);
            RenameTable(name: "dbo.TipoDepoimento", newName: "EventoTipo");
            RenameTable(name: "dbo.Feedback", newName: "DepartamentoDepoimentoes");
        }
    }
}
