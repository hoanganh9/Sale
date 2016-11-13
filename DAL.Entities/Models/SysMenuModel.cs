namespace DAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Base.Common;
    using Base.Lib;
    using Repository.Pattern.Infrastructure;
    using Newtonsoft.Json;
    
    // created : 13/11/2016
    // Author : Generate by Anhhn
    public partial class SysMenuModel : EntityModel<SysMenu>
    {
    	public SysMenuModel()
        {
    		_entity = new SysMenu();
    	}
    
    	public SysMenuModel(SysMenu entity) : base(entity)
        {
        }
    	
    	//TODO: SysMenu-Model
    	
    	[Display(Name = "Id")]
    	public string Id
    	{
    		get{ return _entity.Id; }
    		set{ _entity.Id = value; }
    	}
    
    		
    	[Display(Name = "Name")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[StringLength(256, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Name
    	{
    		get{ return _entity.Name; }
    		set{ _entity.Name = value; }
    	}
    
    		
    	[Display(Name = "ActionCode")]
    	[StringLength(200, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string ActionCode
    	{
    		get{ return _entity.ActionCode; }
    		set{ _entity.ActionCode = value; }
    	}
    
    		
    	[Display(Name = "Area")]
    	[StringLength(20, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Area
    	{
    		get{ return _entity.Area; }
    		set{ _entity.Area = value; }
    	}
    
    		
    	[Display(Name = "Controller")]
    	[StringLength(50, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Controller
    	{
    		get{ return _entity.Controller; }
    		set{ _entity.Controller = value; }
    	}
    
    		
    	[Display(Name = "Action")]
    	[StringLength(50, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Action
    	{
    		get{ return _entity.Action; }
    		set{ _entity.Action = value; }
    	}
    
    		
    	[Display(Name = "Params")]
    	[StringLength(500, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Params
    	{
    		get{ return _entity.Params; }
    		set{ _entity.Params = value; }
    	}
    
    		
    	[Display(Name = "Pram1")]
    	[StringLength(50, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Pram1
    	{
    		get{ return _entity.Pram1; }
    		set{ _entity.Pram1 = value; }
    	}
    
    		
    	[Display(Name = "Pram2")]
    	[StringLength(50, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Pram2
    	{
    		get{ return _entity.Pram2; }
    		set{ _entity.Pram2 = value; }
    	}
    
    		
    	[Display(Name = "Pram3")]
    	[StringLength(50, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Pram3
    	{
    		get{ return _entity.Pram3; }
    		set{ _entity.Pram3 = value; }
    	}
    
    		
    	[Display(Name = "QuerryString")]
    	[StringLength(200, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string QuerryString
    	{
    		get{ return _entity.QuerryString; }
    		set{ _entity.QuerryString = value; }
    	}
    
    		
    	[Display(Name = "Url")]
    	[StringLength(500, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Url
    	{
    		get{ return _entity.Url; }
    		set{ _entity.Url = value; }
    	}
    
    		
    	[Display(Name = "ParentId")]
    	[StringLength(128, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string ParentId
    	{
    		get{ return _entity.ParentId; }
    		set{ _entity.ParentId = value; }
    	}
    
    		
    	[Display(Name = "Order")]
    	[RegularExpression(Enums.RegexDefine.IntergerAm, ErrorMessage = Enums.RegexMessage.Interger)]
    	[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = Enums.FormatModel.Integer)]
    	[DataType(DataType.Text)]
    	[Range(-2147483648, 2147483647, ErrorMessage = Enums.ErrorMessage.RangeMinMax)]
    	public Nullable<int> Order
    	{
    		get{ return _entity.Order; }
    		set{ _entity.Order = value; }
    	}
    
    		
    	[Display(Name = "Description")]
    	[StringLength(500, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Description
    	{
    		get{ return _entity.Description; }
    		set{ _entity.Description = value; }
    	}
    
    		
    	[Display(Name = "MenuType")]
    	[RegularExpression(Enums.RegexDefine.IntergerAm, ErrorMessage = Enums.RegexMessage.Interger)]
    	[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = Enums.FormatModel.Integer)]
    	[DataType(DataType.Text)]
    	[Range(-2147483648, 2147483647, ErrorMessage = Enums.ErrorMessage.RangeMinMax)]
    	public Nullable<int> MenuType
    	{
    		get{ return _entity.MenuType; }
    		set{ _entity.MenuType = value; }
    	}
    
    		
    	[Display(Name = "Icon")]
    	[StringLength(200, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Icon
    	{
    		get{ return _entity.Icon; }
    		set{ _entity.Icon = value; }
    	}
    
    		
    	[Display(Name = "CreateDate")]
    	public System.DateTime CreateDate
    	{
    		get{ return _entity.CreateDate; }
    		set{ _entity.CreateDate = value; }
    	}
    
    		
    	[Display(Name = "CreateBy")]
    	public string CreateBy
    	{
    		get{ return _entity.CreateBy; }
    		set{ _entity.CreateBy = value; }
    	}
    
    		
    	[Display(Name = "ModifyDate")]
    	public Nullable<System.DateTime> ModifyDate
    	{
    		get{ return _entity.ModifyDate; }
    		set{ _entity.ModifyDate = value; }
    	}
    
    		
    	[Display(Name = "ModifyBy")]
    	public string ModifyBy
    	{
    		get{ return _entity.ModifyBy; }
    		set{ _entity.ModifyBy = value; }
    	}
    
    	
        public virtual SysAction SysAction
    	{
    		get{ return _entity.SysAction; }
    		set{ _entity.SysAction = value; }
    	}
    	[JsonIgnore]
        public virtual List<SysMenu> SysMenu1
    	{
    		get{ return _entity.SysMenu1 != null?_entity.SysMenu1.ToList() : new List<SysMenu>(); }
    		set{ _entity.SysMenu1 = value; }
    	}
        public virtual SysMenu SysMenu2
    	{
    		get{ return _entity.SysMenu2; }
    		set{ _entity.SysMenu2 = value; }
    	}
    
    	#region base
    
    	public SysMenu toCreate()
    	{
    		//if(string.IsNullOrEmpty(_entity.Id))
    		//	_entity.Id = Guid.NewGuid().ToString();
    		
    		//_entity.Status = (int)Enums.StatusBase.Active;
    		//_entity.CreateDate = this.DateChanged;
    		//_entity.CreateBy = this.UserId;
    		return _entity;
    	}
    
    	public void changeEdit(SysMenu entityOld)
    	{
    		entityOld.Name = _entity.Name;
    		entityOld.ActionCode = _entity.ActionCode;
    		entityOld.Area = _entity.Area;
    		entityOld.Controller = _entity.Controller;
    		entityOld.Action = _entity.Action;
    		entityOld.Params = _entity.Params;
    		entityOld.Pram1 = _entity.Pram1;
    		entityOld.Pram2 = _entity.Pram2;
    		entityOld.Pram3 = _entity.Pram3;
    		entityOld.QuerryString = _entity.QuerryString;
    		entityOld.Url = _entity.Url;
    		entityOld.ParentId = _entity.ParentId;
    		entityOld.Order = _entity.Order;
    		entityOld.Description = _entity.Description;
    		entityOld.MenuType = _entity.MenuType;
    		entityOld.Icon = _entity.Icon;
    		
    		//entityOld.ModifyDate = this.DateChanged;
    		//entityOld.ModifyBy = this.UserId;
    	}
    	#endregion base
    }
    public partial class SysMenuModelSearch : ModelSearch
    {
        public SysMenuModelSearch() : base()
        {
        }
    }
}