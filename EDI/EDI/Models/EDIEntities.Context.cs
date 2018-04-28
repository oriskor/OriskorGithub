﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EDI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EDIEntitiesCon : DbContext
    {
        public EDIEntitiesCon()
            : base("name=EDIEntitiesCon")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C810_Detail> C810_Detail { get; set; }
        public virtual DbSet<C810_Header> C810_Header { get; set; }
        public virtual DbSet<C850_Detail> C850_Detail { get; set; }
        public virtual DbSet<C850_Header> C850_Header { get; set; }
        public virtual DbSet<C852_Detail> C852_Detail { get; set; }
        public virtual DbSet<C852_Header> C852_Header { get; set; }
        public virtual DbSet<C852_ProdActivity> C852_ProdActivity { get; set; }
        public virtual DbSet<C856_Header> C856_Header { get; set; }
        public virtual DbSet<C856_Item> C856_Item { get; set; }
        public virtual DbSet<C856_Order> C856_Order { get; set; }
        public virtual DbSet<C856_Pack> C856_Pack { get; set; }
        public virtual DbSet<C856_Shipment> C856_Shipment { get; set; }
        public virtual DbSet<C856_Tare> C856_Tare { get; set; }
        public virtual DbSet<C860_Detail> C860_Detail { get; set; }
        public virtual DbSet<C860_Header> C860_Header { get; set; }
        public virtual DbSet<company_master> company_master { get; set; }
        public virtual DbSet<docu_type> docu_type { get; set; }
        public virtual DbSet<docu_type_sub> docu_type_sub { get; set; }
        public virtual DbSet<edi_format_master> edi_format_master { get; set; }
        public virtual DbSet<EDIFile> EDIFiles { get; set; }
        public virtual DbSet<FunctionalGroupInbound> FunctionalGroupInbounds { get; set; }
        public virtual DbSet<FunctionalGroupOutbound> FunctionalGroupOutbounds { get; set; }
        public virtual DbSet<InterchangeInbound> InterchangeInbounds { get; set; }
        public virtual DbSet<InterchangeOutbound> InterchangeOutbounds { get; set; }
        public virtual DbSet<item_setup> item_setup { get; set; }
        public virtual DbSet<menu_master> menu_master { get; set; }
        public virtual DbSet<MenuRoleMapping> MenuRoleMappings { get; set; }
        public virtual DbSet<package_master> package_master { get; set; }
        public virtual DbSet<PermissionType> PermissionTypes { get; set; }
        public virtual DbSet<role_master> role_master { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<sub_menu> sub_menu { get; set; }
        public virtual DbSet<tradingPartnerSetup> tradingPartnerSetups { get; set; }
        public virtual DbSet<tradingPartnerSetup_sub> tradingPartnerSetup_sub { get; set; }
        public virtual DbSet<transaction_rejected> transaction_rejected { get; set; }
        public virtual DbSet<transaction_sent> transaction_sent { get; set; }
        public virtual DbSet<transaction> transactions { get; set; }
        public virtual DbSet<user_master> user_master { get; set; }
        public virtual DbSet<erp_master> erp_master { get; set; }
        public virtual DbSet<subscription_master> subscription_master { get; set; }
    
        public virtual ObjectResult<GetHeaderDetailInformation_Result> GetHeaderDetailInformation()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetHeaderDetailInformation_Result>("GetHeaderDetailInformation");
        }
    
        public virtual ObjectResult<SPO_getAllMenuByUserID_Result> SPO_getAllMenuByUserID(Nullable<int> userID, string userName)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPO_getAllMenuByUserID_Result>("SPO_getAllMenuByUserID", userIDParameter, userNameParameter);
        }
    
        public virtual ObjectResult<SPO_getAllMenuWhichNotAssignedToTheRole_Result> SPO_getAllMenuWhichNotAssignedToTheRole(Nullable<int> roleId)
        {
            var roleIdParameter = roleId.HasValue ?
                new ObjectParameter("RoleId", roleId) :
                new ObjectParameter("RoleId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPO_getAllMenuWhichNotAssignedToTheRole_Result>("SPO_getAllMenuWhichNotAssignedToTheRole", roleIdParameter);
        }
    
        public virtual ObjectResult<SPO_getRejectedFiles_Result> SPO_getRejectedFiles()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPO_getRejectedFiles_Result>("SPO_getRejectedFiles");
        }
    
        public virtual ObjectResult<Nullable<decimal>> SPO_insertEDIFilesDetails(string fileNames, string filePath, string statusCode, string ediFileTranSetNo, string createdDateTime)
        {
            var fileNamesParameter = fileNames != null ?
                new ObjectParameter("fileNames", fileNames) :
                new ObjectParameter("fileNames", typeof(string));
    
            var filePathParameter = filePath != null ?
                new ObjectParameter("FilePath", filePath) :
                new ObjectParameter("FilePath", typeof(string));
    
            var statusCodeParameter = statusCode != null ?
                new ObjectParameter("StatusCode", statusCode) :
                new ObjectParameter("StatusCode", typeof(string));
    
            var ediFileTranSetNoParameter = ediFileTranSetNo != null ?
                new ObjectParameter("EdiFileTranSetNo", ediFileTranSetNo) :
                new ObjectParameter("EdiFileTranSetNo", typeof(string));
    
            var createdDateTimeParameter = createdDateTime != null ?
                new ObjectParameter("CreatedDateTime", createdDateTime) :
                new ObjectParameter("CreatedDateTime", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("SPO_insertEDIFilesDetails", fileNamesParameter, filePathParameter, statusCodeParameter, ediFileTranSetNoParameter, createdDateTimeParameter);
        }
    }
}