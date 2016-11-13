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
    public partial class SysConfigModel : EntityModel<SysConfig>
    {
    	public SysConfigModel()
        {
    		_entity = new SysConfig();
    	}
    
    	public SysConfigModel(SysConfig entity) : base(entity)
        {
        }
    	
    	//TODO: SysConfig-Model
    	
    	[Display(Name = "Code")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[StringLength(50, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Code
    	{
    		get{ return _entity.Code; }
    		set{ _entity.Code = value; }
    	}
    
    		
    	[Display(Name = "Value")]
    	[StringLength(500, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Value
    	{
    		get{ return _entity.Value; }
    		set{ _entity.Value = value; }
    	}
    
    		
    	[Display(Name = "Description")]
    	[StringLength(500, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Description
    	{
    		get{ return _entity.Description; }
    		set{ _entity.Description = value; }
    	}
    
    	
    	#region base
    
    	public SysConfig toCreate()
    	{
    		//if(string.IsNullOrEmpty(_entity.Id))
    		//	_entity.Id = Guid.NewGuid().ToString();
    		
    		//_entity.Status = (int)Enums.StatusBase.Active;
    		//_entity.CreateDate = this.DateChanged;
    		//_entity.CreateBy = this.UserId;
    		return _entity;
    	}
    
    	public void changeEdit(SysConfig entityOld)
    	{
    		entityOld.Code = _entity.Code;
    		entityOld.Value = _entity.Value;
    		entityOld.Description = _entity.Description;
    		
    		//entityOld.ModifyDate = this.DateChanged;
    		//entityOld.ModifyBy = this.UserId;
    	}
    	#endregion base
    }
    public partial class SysConfigModelSearch : ModelSearch
    {
        public SysConfigModelSearch() : base()
        {
        }
    }
}