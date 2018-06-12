namespace Ouvidoria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Curso",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Departamento",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.DepartamentoDepoimentoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Avaliacao = c.Int(nullable: false),
                        Comentario = c.String(maxLength: 200),
                        idUsuario = c.Int(nullable: false),
                        idDepartamento = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Departamento", t => t.idDepartamento, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.idUsuario, cascadeDelete: true)
                .Index(t => t.idUsuario)
                .Index(t => t.idDepartamento);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Telefone = c.String(maxLength: 15),
                        Senha = c.String(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        idCurso = c.Int(nullable: false),
                        idUsuarioPerfil = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Curso", t => t.idCurso, cascadeDelete: true)
                .ForeignKey("dbo.UsuarioPerfil", t => t.idUsuarioPerfil, cascadeDelete: true)
                .Index(t => t.idCurso)
                .Index(t => t.idUsuarioPerfil);
            
            CreateTable(
                "dbo.UsuarioPerfil",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Perfil = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
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
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.EventoTipo", t => t.idEventoTipo, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.idUsuario, cascadeDelete: true)
                .Index(t => t.idEventoTipo)
                .Index(t => t.idUsuario);
            
            CreateTable(
                "dbo.EventoTipo",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Tipo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Evento", "idUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Evento", "idEventoTipo", "dbo.EventoTipo");
            DropForeignKey("dbo.DepartamentoDepoimentoes", "idUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Usuario", "idUsuarioPerfil", "dbo.UsuarioPerfil");
            DropForeignKey("dbo.Usuario", "idCurso", "dbo.Curso");
            DropForeignKey("dbo.DepartamentoDepoimentoes", "idDepartamento", "dbo.Departamento");
            DropIndex("dbo.Evento", new[] { "idUsuario" });
            DropIndex("dbo.Evento", new[] { "idEventoTipo" });
            DropIndex("dbo.Usuario", new[] { "idUsuarioPerfil" });
            DropIndex("dbo.Usuario", new[] { "idCurso" });
            DropIndex("dbo.DepartamentoDepoimentoes", new[] { "idDepartamento" });
            DropIndex("dbo.DepartamentoDepoimentoes", new[] { "idUsuario" });
            DropTable("dbo.EventoTipo");
            DropTable("dbo.Evento");
            DropTable("dbo.UsuarioPerfil");
            DropTable("dbo.Usuario");
            DropTable("dbo.DepartamentoDepoimentoes");
            DropTable("dbo.Departamento");
            DropTable("dbo.Curso");
        }
    }
}
