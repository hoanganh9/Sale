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
    public partial class SysLogModel : EntityModel<SysLog>
    {
    	public SysLogModel()
        {
    		_entity = new SysLog();
    	}
    
    	public SysLogModel(SysLog entity) : base(entity)
        {
        }
    	
    	//TODO: SysLog-Model
    	
    	[Display(Name = "LogId")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[StringLength(128, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string LogId
    	{
    		get{ return _entity.LogId; }
    		set{ _entity.LogId = value; }
    	}
    
    		
    	[Display(Name = "UserChange")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[StringLength(128, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string UserChange
    	{
    		get{ return _entity.UserChange; }
    		set{ _entity.UserChange = value; }
    	}
    
    		
    	[Display(Name = "DateChange")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = Enums.FormatModel.FormatDateVN)]
    	public System.DateTime DateChange
    	{
    		get{ return _entity.DateChange; }
    		set{ _entity.DateChange = value; }
    	}
    	[Display(Name = "DateChange")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = Enums.FormatModel.FormatDateVN)]
    	[DataType(DataType.Date)]
    	public string _DateChange
    	{
    		set { _entity.DateChange = Validate.ConvertDateVN(value); }
    		get { return _entity.DateChange == DateTime.MinValue ? string.Empty : _entity.DateChange.ToString(Enums.FormatType.FormatDateVN); }
    	}
    
    		
    	[Display(Name = "ChangeInfo")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	public string ChangeInfo
    	{
    		get{ return _entity.ChangeInfo; }
    		set{ _entity.ChangeInfo = value; }
    	}
    
    		
    	[Display(Name = "LogInfo")]
    	public string LogInfo
    	{
    		get{ return _entity.LogInfo; }
    		set{ _entity.LogInfo = value; }
    	}
    
    	
    	#region base
    
    	public SysLog toCreate()
    	{
    		//if(string.IsNullOrEmpty(_entity.Id))
    		//	_entity.Id = Guid.NewGuid().ToString();
    		
    		//_entity.Status = (int)Enums.StatusBase.Active;
    		//_entity.CreateDate = this.DateChanged;
    		//_entity.CreateBy = this.UserId;
    		return _entity;
    	}
    
    	public void changeEdit(SysLog entityOld)
    	{
    		entityOld.LogId = _entity.LogId;
    		entityOld.UserChange = _entity.UserChange;
    		entityOld.DateChange = _entity.DateChange;
    		entityOld.ChangeInfo = _entity.ChangeInfo;
    		entityOld.LogInfo = _entity.LogInfo;
    		
    		//entityOld.ModifyDate = this.DateChanged;
    		//entityOld.ModifyBy = this.UserId;
    	}
    	#endregion base
    }
    public partial class SysLogModelSearch : ModelSearch
    {
        public SysLogModelSearch() : base()
        {
        }
    }
}