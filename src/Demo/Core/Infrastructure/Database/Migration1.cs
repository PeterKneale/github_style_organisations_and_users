using FluentMigrator;

namespace Demo.Core.Infrastructure.Database;

[Migration(1)]
public class Migration1 : Migration
{
    public override void Up()
    {
        Create.Table("organisations")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("title").AsString()
            .WithColumn("description").AsString().Nullable();

        Create.Table("organisation_member")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("organisation_id").AsGuid()
            .WithColumn("member_id").AsGuid()
            .WithColumn("role").AsString();
            
        Create.Table("users")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("first_name").AsString()
            .WithColumn("last_name").AsString()
            .WithColumn("email").AsString()
            .WithColumn("password").AsString();
    }

    public override void Down()
    {
        DropTableIfExists("organisation_member");
        DropTableIfExists("users");
        DropTableIfExists("organisations");
    }
    
    private void DropTableIfExists(string tableName)
    {
        Execute.Sql($"DROP TABLE IF EXISTS {tableName};");
    }
}