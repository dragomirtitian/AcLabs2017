namespace Game.Mvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnUpgradeCompletedAt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mine", "UpgradeCompletedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mine", "UpgradeCompletedAt");
        }
    }
}
