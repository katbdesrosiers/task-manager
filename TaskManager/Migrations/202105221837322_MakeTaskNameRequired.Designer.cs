﻿// <auto-generated />
namespace TaskManager.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.4.4")]
    public sealed partial class MakeTaskNameRequired : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(MakeTaskNameRequired));
        
        string IMigrationMetadata.Id
        {
            get { return "202105221837322_MakeTaskNameRequired"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
