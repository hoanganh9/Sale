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
    public partial class SysGenCodeModel : EntityModel<SysGenCode>
    {
    	public SysGenCodeModel()
        {
    		_entity = new SysGenCode();
    	}
    
    	public SysGenCodeModel(SysGenCode entity) : base(entity)
        {
        }
    	
    	//TODO: SysGenCode-Model
    	
    	[Display(Name = "TableName")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[StringLength(200, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string TableName
    	{
    		get{ return _entity.TableName; }
    		set{ _entity.TableName = value; }
    	}
    
    		
    	[Display(Name = "Prefix")]
    	[StringLength(10, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Prefix
    	{
    		get{ return _entity.Prefix; }
    		set{ _entity.Prefix = value; }
    	}
    
    		
    	[Display(Name = "CurentDate")]
    	[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = Enums.FormatModel.FormatDateVN)]
    	public Nullable<System.DateTime> CurentDate
    	{
    		get{ return _entity.CurentDate; }
    		set{ _entity.CurentDate = value; }
    	}
    	[Display(Name = "CurentDate")]
    	[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = Enums.FormatModel.FormatDateVN)]
    	[DataType(DataType.Date)]
    	public string _CurentDate
    	{
    		set { _entity.CurentDate = string.IsNullOrWhiteSpace(value)? null : Validate.ConvertDateVNAlowNull(value); }
    		get { return _entity.CurentDate.HasValue? _entity.CurentDate.Value.ToString(Enums.FormatType.FormatDateVN) : string.Empty; }
    	}
    
    		
    	[Display(Name = "FormatDate")]
    	[StringLength(10, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string FormatDate
    	{
    		get{ return _entity.FormatDate; }
    		set{ _entity.FormatDate = value; }
    	}
    
    		
    	[Display(Name = "Length")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[RegularExpression(Enums.RegexDefine.IntergerAm, ErrorMessage = Enums.RegexMessage.Interger)]
    	[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = Enums.FormatModel.Integer)]
    	[DataType(DataType.Text)]
    	[Range(-2147483648, 2147483647, ErrorMessage = Enums.ErrorMessage.RangeMinMax)]
    	public int Length
    	{
    		get{ return _entity.Length; }
    		set{ _entity.Length = value; }
    	}
    
    		
    	[Display(Name = "CurentIndex")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[RegularExpression(Enums.RegexDefine.IntergerAm, ErrorMessage = Enums.RegexMessage.Interger)]
    	[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = Enums.FormatModel.Integer)]
    	[DataType(DataType.Text)]
    	[Range(-2147483648, 2147483647, ErrorMessage = Enums.ErrorMessage.RangeMinMax)]
    	public int CurentIndex
    	{
    		get{ return _entity.CurentIndex; }
    		set{ _entity.CurentIndex = value; }
    	}
    
    		
    	[Display(Name = "CurentAlphabet")]
    	[StringLength(10, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string CurentAlphabet
    	{
    		get{ return _entity.CurentAlphabet; }
    		set{ _entity.CurentAlphabet = value; }
    	}
    
    		
    	[Display(Name = "UsingAlphabet")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	public bool UsingAlphabet
    	{
    		get{ return _entity.UsingAlphabet; }
    		set{ _entity.UsingAlphabet = value; }
    	}
    
    		
    	[Display(Name = "TypeReset")]
    	[StringLength(4, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string TypeReset
    	{
    		get{ return _entity.TypeReset; }
    		set{ _entity.TypeReset = value; }
    	}
    
    		
    	[Display(Name = "FormatCode")]
    	[StringLength(500, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string FormatCode
    	{
    		get{ return _entity.FormatCode; }
    		set{ _entity.FormatCode = value; }
    	}
    
    		
    	[Display(Name = "FormatKey")]
    	[StringLength(500, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string FormatKey
    	{
    		get{ return _entity.FormatKey; }
    		set{ _entity.FormatKey = value; }
    	}
    
    	
    	#region base
    
    	public SysGenCode toCreate()
    	{
    		//if(string.IsNullOrEmpty(_entity.Id))
    		//	_entity.Id = Guid.NewGuid().ToString();
    		
    		//_entity.Status = (int)Enums.StatusBase.Active;
    		//_entity.CreateDate = this.DateChanged;
    		//_entity.CreateBy = this.UserId;
    		return _entity;
    	}
    
    	public void changeEdit(SysGenCode entityOld)
    	{
    		entityOld.TableName = _entity.TableName;
    		entityOld.Prefix = _entity.Prefix;
    		entityOld.CurentDate = _entity.CurentDate;
    		entityOld.FormatDate = _entity.FormatDate;
    		entityOld.Length = _entity.Length;
    		entityOld.CurentIndex = _entity.CurentIndex;
    		entityOld.CurentAlphabet = _entity.CurentAlphabet;
    		entityOld.UsingAlphabet = _entity.UsingAlphabet;
    		entityOld.TypeReset = _entity.TypeReset;
    		entityOld.FormatCode = _entity.FormatCode;
    		entityOld.FormatKey = _entity.FormatKey;
    		
    		//entityOld.ModifyDate = this.DateChanged;
    		//entityOld.ModifyBy = this.UserId;
    	}
    	#endregion base
    }
    public partial class SysGenCodeModelSearch : ModelSearch
    {
        public SysGenCodeModelSearch() : base()
        {
        }
    }
}