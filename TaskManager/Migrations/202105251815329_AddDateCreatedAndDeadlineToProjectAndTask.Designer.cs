﻿// <auto-generated />
namespace TaskManager.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.4.4")]
    public sealed partial class AddDateCreatedAndDeadlineToProjectAndTask : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(AddDateCreatedAndDeadlineToProjectAndTask));
        
        string IMigrationMetadata.Id
        {
            get { return "202105251815329_AddDateCreatedAndDeadlineToProjectAndTask"; }
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