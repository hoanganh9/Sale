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
    public partial class SysActionModel : EntityModel<SysAction>
    {
    	public SysActionModel()
        {
    		_entity = new SysAction();
    	}
    
    	public SysActionModel(SysAction entity) : base(entity)
        {
        }
    	
    	//TODO: SysAction-Model
    	
    	[Display(Name = "Code")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[StringLength(200, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Code
    	{
    		get{ return _entity.Code; }
    		set{ _entity.Code = value; }
    	}
    
    		
    	[Display(Name = "Controller")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[StringLength(50, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Controller
    	{
    		get{ return _entity.Controller; }
    		set{ _entity.Controller = value; }
    	}
    
    		
    	[Display(Name = "Action")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[StringLength(50, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Action
    	{
    		get{ return _entity.Action; }
    		set{ _entity.Action = value; }
    	}
    
    		
    	[Display(Name = "Area")]
    	[StringLength(20, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Area
    	{
    		get{ return _entity.Area; }
    		set{ _entity.Area = value; }
    	}
    
    		
    	[Display(Name = "Description")]
    	[StringLength(500, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Description
    	{
    		get{ return _entity.Description; }
    		set{ _entity.Description = value; }
    	}
    
    		
    	[Display(Name = "Params")]
    	[StringLength(500, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Params
    	{
    		get{ return _entity.Params; }
    		set{ _entity.Params = value; }
    	}
    
    		
    	[Display(Name = "IsMenu")]
    	public Nullable<bool> IsMenu
    	{
    		get{ return _entity.IsMenu; }
    		set{ _entity.IsMenu = value; }
    	}
    
    		
    	[Display(Name = "Component")]
    	[StringLength(50, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Component
    	{
    		get{ return _entity.Component; }
    		set{ _entity.Component = value; }
    	}
    
    		
    	[Display(Name = "Icon")]
    	[StringLength(200, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Icon
    	{
    		get{ return _entity.Icon; }
    		set{ _entity.Icon = value; }
    	}
    
    		
    	[Display(Name = "ControllerDesc")]
    	[StringLength(200, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string ControllerDesc
    	{
    		get{ return _entity.ControllerDesc; }
    		set{ _entity.ControllerDesc = value; }
    	}
    
    		
    	[Display(Name = "ComponentDesc")]
    	[StringLength(200, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string ComponentDesc
    	{
    		get{ return _entity.ComponentDesc; }
    		set{ _entity.ComponentDesc = value; }
    	}
    
    		
    	[Display(Name = "AreaDesc")]
    	[StringLength(200, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string AreaDesc
    	{
    		get{ return _entity.AreaDesc; }
    		set{ _entity.AreaDesc = value; }
    	}
    
    	
    	[JsonIgnore]
        public virtual List<SysMenu> SysMenus
    	{
    		get{ return _entity.SysMenus != null?_entity.SysMenus.ToList() : new List<SysMenu>(); }
    		set{ _entity.SysMenus = value; }
    	}
    	[JsonIgnore]
        public virtual List<SysGroupAction> SysGroupActions
    	{
    		get{ return _entity.SysGroupActions != null?_entity.SysGroupActions.ToList() : new List<SysGroupAction>(); }
    		set{ _entity.SysGroupActions = value; }
    	}
    	[JsonIgnore]
        public virtual List<AspNetRole> AspNetRoles
    	{
    		get{ return _entity.AspNetRoles != null?_entity.AspNetRoles.ToList() : new List<AspNetRole>(); }
    		set{ _entity.AspNetRoles = value; }
    	}
    
    	#region base
    
    	public SysAction toCreate()
    	{
    		//if(string.IsNullOrEmpty(_entity.Id))
    		//	_entity.Id = Guid.NewGuid().ToString();
    		
    		//_entity.Status = (int)Enums.StatusBase.Active;
    		//_entity.CreateDate = this.DateChanged;
    		//_entity.CreateBy = this.UserId;
    		return _entity;
    	}
    
    	public void changeEdit(SysAction entityOld)
    	{
    		entityOld.Code = _entity.Code;
    		entityOld.Controller = _entity.Controller;
    		entityOld.Action = _entity.Action;
    		entityOld.Area = _entity.Area;
    		entityOld.Description = _entity.Description;
    		entityOld.Params = _entity.Params;
    		entityOld.IsMenu = _entity.IsMenu;
    		entityOld.Component = _entity.Component;
    		entityOld.Icon = _entity.Icon;
    		entityOld.ControllerDesc = _entity.ControllerDesc;
    		entityOld.ComponentDesc = _entity.ComponentDesc;
    		entityOld.AreaDesc = _entity.AreaDesc;
    		
    		//entityOld.ModifyDate = this.DateChanged;
    		//entityOld.ModifyBy = this.UserId;
    	}
    	#endregion base
    }
    public partial class SysActionModelSearch : ModelSearch
    {
        public SysActionModelSearch() : base()
        {
        }
    }
}