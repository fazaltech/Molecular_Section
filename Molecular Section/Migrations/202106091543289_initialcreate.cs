namespace Molecular_Section.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.idrl",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        site_id = c.Int(nullable: false),
                        specimen = c.String(),
                        datesample = c.String(),
                        timesample = c.String(),
                        receivedby = c.String(),
                        temloggerinclude = c.String(),
                        tempcoleman = c.String(),
                        samplereceipt = c.String(),
                        laboratoryremark = c.String(),
                        rnaextractiondate = c.String(),
                        rnaremark = c.String(),
                        realtimepcrn1ddmmyy = c.String(),
                        realtimepcrn1gen = c.String(),
                        ctvaluep1 = c.String(),
                        n1gneremark = c.String(),
                        realtimepcrn2ddmmyy = c.String(),
                        realtimepcrn2gen = c.String(),
                        ctvaluep2 = c.String(),
                        n2gneremark = c.String(),
                        realtimepcrnEgen = c.String(),
                        realtimepcrn3Egen = c.String(),
                        ctvaluep3 = c.String(),
                        n_e_gneremark = c.String(),
                        target1gcl = c.String(),
                        target2gcl = c.String(),
                        target1limitgcl = c.String(),
                        target2limitgcl = c.String(),
                        target1pre_absen = c.String(),
                        target2pre_absen = c.String(),
                        flowm3day = c.String(),
                        passqaqc = c.String(),
                        waterquality = c.String(),
                        watervalue = c.String(),
                        collectmethod = c.String(),
                        note = c.String(),
                        entry_date = c.String(),
                        user_name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.site",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        site_id = c.Int(nullable: false),
                        site_name = c.String(),
                        colflag = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.specimen",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        site_id = c.Int(nullable: false),
                        specimen = c.String(),
                        colflag = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.user_dash",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        user_name = c.String(),
                        email_id = c.String(),
                        password = c.String(),
                        role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.user_dash");
            DropTable("dbo.specimen");
            DropTable("dbo.site");
            DropTable("dbo.idrl");
        }
    }
}
