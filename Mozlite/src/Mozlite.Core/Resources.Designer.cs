﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mozlite {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///    强类型资源类，用于查找本地化字符串，等等。
    /// </summary>
    // 此类已由 StronglyTypedResourceBuilder 自动生成
    // 通过 ResGen 或 Visual Studio 之类的工具提供的类。
    // 若要添加或删除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (使用 /str 选项)，或重新生成 VS 项目。
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        internal Resources() {
        }
        
        /// <summary>
        ///    返回此类使用的缓存 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Mozlite.Core.Resources", typeof(Resources).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///    重写所有项的当前线程的 CurrentUICulture 属性
        ///    使用此强类型资源类进行资源查找。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///    查找与 管理员 类似的本地化字符串。
        /// </summary>
        public static string Administrator {
            get {
                return ResourceManager.GetString("Administrator", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 The string argument &apos;{0}&apos; cannot be empty. 类似的本地化字符串。
        /// </summary>
        public static string ArgumentIsEmpty {
            get {
                return ResourceManager.GetString("ArgumentIsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 The property &apos;{0}&apos; of the argument &apos;{1}&apos; cannot be null. 类似的本地化字符串。
        /// </summary>
        public static string ArgumentPropertyNull {
            get {
                return ResourceManager.GetString("ArgumentPropertyNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 完成 类似的本地化字符串。
        /// </summary>
        public static string ArgumentStatus_Completed {
            get {
                return ResourceManager.GetString("ArgumentStatus_Completed", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 禁用 类似的本地化字符串。
        /// </summary>
        public static string ArgumentStatus_Disabled {
            get {
                return ResourceManager.GetString("ArgumentStatus_Disabled", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 失败 类似的本地化字符串。
        /// </summary>
        public static string ArgumentStatus_Failured {
            get {
                return ResourceManager.GetString("ArgumentStatus_Failured", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 正常 类似的本地化字符串。
        /// </summary>
        public static string ArgumentStatus_Normal {
            get {
                return ResourceManager.GetString("ArgumentStatus_Normal", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 浏览文件 类似的本地化字符串。
        /// </summary>
        public static string BrowserTitle {
            get {
                return ResourceManager.GetString("BrowserTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 The collection argument &apos;{0}&apos; must contain at least one element. 类似的本地化字符串。
        /// </summary>
        public static string CollectionArgumentIsEmpty {
            get {
                return ResourceManager.GetString("CollectionArgumentIsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 恭喜你，你已经成功添加了“{0}”！ 类似的本地化字符串。
        /// </summary>
        public static string DataAction_Created {
            get {
                return ResourceManager.GetString("DataAction_Created", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 很抱歉，添加“{0}”失败了！ 类似的本地化字符串。
        /// </summary>
        public static string DataAction_CreatedFailured {
            get {
                return ResourceManager.GetString("DataAction_CreatedFailured", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 恭喜你，你已经成功删除了所选择的{0}！ 类似的本地化字符串。
        /// </summary>
        public static string DataAction_Deleted {
            get {
                return ResourceManager.GetString("DataAction_Deleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 很抱歉，删除“{0}”失败了！ 类似的本地化字符串。
        /// </summary>
        public static string DataAction_DeletedFailured {
            get {
                return ResourceManager.GetString("DataAction_DeletedFailured", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 很抱歉，“{0}”已经存在，操作失败! 类似的本地化字符串。
        /// </summary>
        public static string DataAction_Duplicate {
            get {
                return ResourceManager.GetString("DataAction_Duplicate", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 恭喜你，你已经成功完成了“{0}”！ 类似的本地化字符串。
        /// </summary>
        public static string DataAction_Success {
            get {
                return ResourceManager.GetString("DataAction_Success", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 很抱歉，发生了未知错误，操作失败，请重试！ 类似的本地化字符串。
        /// </summary>
        public static string DataAction_UnknownError {
            get {
                return ResourceManager.GetString("DataAction_UnknownError", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 恭喜你，你已经成功更新了“{0}”！ 类似的本地化字符串。
        /// </summary>
        public static string DataAction_Updated {
            get {
                return ResourceManager.GetString("DataAction_Updated", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 很抱歉，更新“{0}”失败了！ 类似的本地化字符串。
        /// </summary>
        public static string DataAction_UpdatedFailured {
            get {
                return ResourceManager.GetString("DataAction_UpdatedFailured", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 &apos;{0}&apos;添加错误，因为已经存在一个同样名称的扩展属性！ 类似的本地化字符串。
        /// </summary>
        public static string DuplicateAnnotation {
            get {
                return ResourceManager.GetString("DuplicateAnnotation", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 The entity type &apos;{0}&apos; provided for the argument &apos;{1}&apos; must be a reference type. 类似的本地化字符串。
        /// </summary>
        public static string InvalidEntityType {
            get {
                return ResourceManager.GetString("InvalidEntityType", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 The properties expression &apos;{0}&apos; is not valid. The expression should represent a property access: &apos;t =&gt; t.MyProperty&apos;. When specifying multiple properties use an anonymous type: &apos;t =&gt; new {{ t.MyProperty1, t.MyProperty2 }}&apos;. 类似的本地化字符串。
        /// </summary>
        public static string InvalidPropertiesExpression {
            get {
                return ResourceManager.GetString("InvalidPropertiesExpression", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 The expression &apos;{0}&apos; is not a valid property expression. The expression should represent a property access: &apos;t =&gt; t.MyProperty&apos;. 类似的本地化字符串。
        /// </summary>
        public static string InvalidPropertyExpression {
            get {
                return ResourceManager.GetString("InvalidPropertyExpression", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 上一页 类似的本地化字符串。
        /// </summary>
        public static string LastPage {
            get {
                return ResourceManager.GetString("LastPage", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 上传路径不正确，当前文件所属实例不明确！ 类似的本地化字符串。
        /// </summary>
        public static string MediaFileUploadPathIsError {
            get {
                return ResourceManager.GetString("MediaFileUploadPathIsError", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 The migration is error:{0}. 类似的本地化字符串。
        /// </summary>
        public static string MigrationError {
            get {
                return ResourceManager.GetString("MigrationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 下一页 类似的本地化字符串。
        /// </summary>
        public static string NextPage {
            get {
                return ResourceManager.GetString("NextPage", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 The property &apos;{0}&apos; of entity type &apos;{1}&apos; does not have a getter.  类似的本地化字符串。
        /// </summary>
        public static string NoGetter {
            get {
                return ResourceManager.GetString("NoGetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 The property &apos;{0}&apos; of entity type &apos;{1}&apos; does not have a setter. 类似的本地化字符串。
        /// </summary>
        public static string NoSetter {
            get {
                return ResourceManager.GetString("NoSetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 第{0}页 类似的本地化字符串。
        /// </summary>
        public static string NumberPage {
            get {
                return ResourceManager.GetString("NumberPage", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 禁用 类似的本地化字符串。
        /// </summary>
        public static string ObjectStatus_Disabled {
            get {
                return ResourceManager.GetString("ObjectStatus_Disabled", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 正常 类似的本地化字符串。
        /// </summary>
        public static string ObjectStatus_Normal {
            get {
                return ResourceManager.GetString("ObjectStatus_Normal", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 等待验证 类似的本地化字符串。
        /// </summary>
        public static string ObjectStatus_PaddingApproved {
            get {
                return ResourceManager.GetString("ObjectStatus_PaddingApproved", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 采集 类似的本地化字符串。
        /// </summary>
        public static string ObjectStatus_Spider {
            get {
                return ResourceManager.GetString("ObjectStatus_Spider", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 请先选择文件后在进行上传！ 类似的本地化字符串。
        /// </summary>
        public static string PleaseSelectFirstAndThenUpload {
            get {
                return ResourceManager.GetString("PleaseSelectFirstAndThenUpload", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 表格的主键已经设置过了，不能重复设置！ 类似的本地化字符串。
        /// </summary>
        public static string PrimaryKeyIsAlreadySetted {
            get {
                return ResourceManager.GetString("PrimaryKeyIsAlreadySetted", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 会员 类似的本地化字符串。
        /// </summary>
        public static string Register {
            get {
                return ResourceManager.GetString("Register", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 [服务]{0}执行错误：{1}。 类似的本地化字符串。
        /// </summary>
        public static string TaskExecuteError {
            get {
                return ResourceManager.GetString("TaskExecuteError", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 The operation of &apos;{0}&apos; cannot be found in &apos;{1}&apos;. 类似的本地化字符串。
        /// </summary>
        public static string UnknownOperation {
            get {
                return ResourceManager.GetString("UnknownOperation", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 实体“{0}”不支持递归查询！ 类似的本地化字符串。
        /// </summary>
        public static string UnsupportedRecurse {
            get {
                return ResourceManager.GetString("UnsupportedRecurse", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 The data type of &apos;{0}&apos; is not supported. 类似的本地化字符串。
        /// </summary>
        public static string UnsupportedType {
            get {
                return ResourceManager.GetString("UnsupportedType", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 上传文件失败，请重试！ 类似的本地化字符串。
        /// </summary>
        public static string UploadFailured {
            get {
                return ResourceManager.GetString("UploadFailured", resourceCulture);
            }
        }
        
        /// <summary>
        ///    查找与 你已经成功上传了文件！ 类似的本地化字符串。
        /// </summary>
        public static string UploadSuccess {
            get {
                return ResourceManager.GetString("UploadSuccess", resourceCulture);
            }
        }
    }
}
