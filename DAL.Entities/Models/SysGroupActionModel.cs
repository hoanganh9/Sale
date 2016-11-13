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
    public partial class SysGroupActionModel : EntityModel<SysGroupAction>
    {
    	public SysGroupActionModel()
        {
    		_entity = new SysGroupAction();
    	}
    
    	public SysGroupActionModel(SysGroupAction entity) : base(entity)
        {
        }
    	
    	//TODO: SysGroupAction-Model
    	
    	[Display(Name = "Id")]
    	public string Id
    	{
    		get{ return _entity.Id; }
    		set{ _entity.Id = value; }
    	}
    
    		
    	[Display(Name = "Code")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[StringLength(50, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Code
    	{
    		get{ return _entity.Code; }
    		set{ _entity.Code = value; }
    	}
    
    		
    	[Display(Name = "Name")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[StringLength(256, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Name
    	{
    		get{ return _entity.Name; }
    		set{ _entity.Name = value; }
    	}
    
    		
    	[Display(Name = "Description")]
    	[StringLength(500, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Description
    	{
    		get{ return _entity.Description; }
    		set{ _entity.Description = value; }
    	}
    
    		
    	[Display(Name = "Level")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[RegularExpression(Enums.RegexDefine.IntergerAm, ErrorMessage = Enums.RegexMessage.Interger)]
    	[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = Enums.FormatModel.Integer)]
    	[DataType(DataType.Text)]
    	[Range(-2147483648, 2147483647, ErrorMessage = Enums.ErrorMessage.RangeMinMax)]
    	public int Level
    	{
    		get{ return _entity.Level; }
    		set{ _entity.Level = value; }
    	}
    
    		
    	[Display(Name = "Status")]
    	public int Status
    	{
    		get{ return _entity.Status; }
    		set{ _entity.Status = value; }
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
    
    	
    	[JsonIgnore]
        public virtual List<SysAction> SysActions
    	{
    		get{ return _entity.SysActions != null?_entity.SysActions.ToList() : new List<SysAction>(); }
    		set{ _entity.SysActions = value; }
    	}
    	[JsonIgnore]
        public virtual List<AspNetRole> AspNetRoles
    	{
    		get{ return _entity.AspNetRoles != null?_entity.AspNetRoles.ToList() : new List<AspNetRole>(); }
    		set{ _entity.AspNetRoles = value; }
    	}
    
    	#region base
    
    	public SysGroupAction toCreate()
    	{
    		//if(string.IsNullOrEmpty(_entity.Id))
    		//	_entity.Id = Guid.NewGuid().ToString();
    		
    		//_entity.Status = (int)Enums.StatusBase.Active;
    		//_entity.CreateDate = this.DateChanged;
    		//_entity.CreateBy = this.UserId;
    		return _entity;
    	}
    
    	public void changeEdit(SysGroupAction entityOld)
    	{
    		entityOld.Code = _entity.Code;
    		entityOld.Name = _entity.Name;
    		entityOld.Description = _entity.Description;
    		entityOld.Level = _entity.Level;
    		
    		//entityOld.ModifyDate = this.DateChanged;
    		//entityOld.ModifyBy = this.UserId;
    	}
    	#endregion base
    }
    public partial class SysGroupActionModelSearch : ModelSearch
    {
        public SysGroupActionModelSearch() : base()
        {
        }
    }
}