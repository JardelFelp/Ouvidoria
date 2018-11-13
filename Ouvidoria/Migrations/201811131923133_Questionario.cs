namespace Ouvidoria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Questionario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Opcao",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        Pergunta_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Pergunta", t => t.Pergunta_id)
                .Index(t => t.Pergunta_id);
            
            CreateTable(
                "dbo.Pergunta",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        Questionario_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Questionario", t => t.Questionario_id)
                .Index(t => t.Questionario_id);
            
            CreateTable(
                "dbo.Questionario",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false),
                        Descricao = c.String(),
                        DataInicio = c.DateTime(nullable: false),
                        DataFim = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Resposta",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Conclusao = c.String(),
                        idOpcao = c.Int(),
                        idPergunta = c.Int(nullable: false),
                        Retorno_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Opcao", t => t.idOpcao)
                .ForeignKey("dbo.Pergunta", t => t.idPergunta, cascadeDelete: true)
                .ForeignKey("dbo.Retorno", t => t.Retorno_id)
                .Index(t => t.idOpcao)
                .Index(t => t.idPergunta)
                .Index(t => t.Retorno_id);
            
            CreateTable(
                "dbo.Retorno",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idQuestionario = c.Int(nullable: false),
                        idUsuario = c.Int(nullable: false),
                        DataRetorno = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Questionario", t => t.idQuestionario, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.idUsuario, cascadeDelete: true)
                .Index(t => t.idQuestionario)
                .Index(t => t.idUsuario);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Retorno", "idUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Resposta", "Retorno_id", "dbo.Retorno");
            DropForeignKey("dbo.Retorno", "idQuestionario", "dbo.Questionario");
            DropForeignKey("dbo.Resposta", "idPergunta", "dbo.Pergunta");
            DropForeignKey("dbo.Resposta", "idOpcao", "dbo.Opcao");
            DropForeignKey("dbo.Pergunta", "Questionario_id", "dbo.Questionario");
            DropForeignKey("dbo.Opcao", "Pergunta_id", "dbo.Pergunta");
            DropIndex("dbo.Retorno", new[] { "idUsuario" });
            DropIndex("dbo.Retorno", new[] { "idQuestionario" });
            DropIndex("dbo.Resposta", new[] { "Retorno_id" });
            DropIndex("dbo.Resposta", new[] { "idPergunta" });
            DropIndex("dbo.Resposta", new[] { "idOpcao" });
            DropIndex("dbo.Pergunta", new[] { "Questionario_id" });
            DropIndex("dbo.Opcao", new[] { "Pergunta_id" });
            DropTable("dbo.Retorno");
            DropTable("dbo.Resposta");
            DropTable("dbo.Questionario");
            DropTable("dbo.Pergunta");
            DropTable("dbo.Opcao");
        }
    }
}
