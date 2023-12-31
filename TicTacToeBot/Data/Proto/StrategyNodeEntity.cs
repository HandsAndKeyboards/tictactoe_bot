// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: strategy_node_entity.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace TestDotnet.Data.Entities {

  /// <summary>Holder for reflection information generated from strategy_node_entity.proto</summary>
  public static partial class StrategyNodeEntityReflection {

    #region Descriptor
    /// <summary>File descriptor for strategy_node_entity.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static StrategyNodeEntityReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChpzdHJhdGVneV9ub2RlX2VudGl0eS5wcm90bxIZdGVzdF9kb3RuZXQuRGF0",
            "YS5FbnRpdGllcyLpAQoSU3RyYXRlZ3lOb2RlRW50aXR5Eg8KB0NvbnRlbnQY",
            "ASABKA0SDQoFSXNLZXkYAiABKAgSUQoKQ2hpbGROb2RlcxgDIAMoCzI9LnRl",
            "c3RfZG90bmV0LkRhdGEuRW50aXRpZXMuU3RyYXRlZ3lOb2RlRW50aXR5LkNo",
            "aWxkTm9kZXNFbnRyeRpgCg9DaGlsZE5vZGVzRW50cnkSCwoDa2V5GAEgASgN",
            "EjwKBXZhbHVlGAIgASgLMi0udGVzdF9kb3RuZXQuRGF0YS5FbnRpdGllcy5T",
            "dHJhdGVneU5vZGVFbnRpdHk6AjgBYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::TestDotnet.Data.Entities.StrategyNodeEntity), global::TestDotnet.Data.Entities.StrategyNodeEntity.Parser, new[]{ "Content", "IsKey", "ChildNodes" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { null, })
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class StrategyNodeEntity : pb::IMessage<StrategyNodeEntity>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<StrategyNodeEntity> _parser = new pb::MessageParser<StrategyNodeEntity>(() => new StrategyNodeEntity());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<StrategyNodeEntity> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::TestDotnet.Data.Entities.StrategyNodeEntityReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public StrategyNodeEntity() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public StrategyNodeEntity(StrategyNodeEntity other) : this() {
      content_ = other.content_;
      isKey_ = other.isKey_;
      childNodes_ = other.childNodes_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public StrategyNodeEntity Clone() {
      return new StrategyNodeEntity(this);
    }

    /// <summary>Field number for the "Content" field.</summary>
    public const int ContentFieldNumber = 1;
    private uint content_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint Content {
      get { return content_; }
      set {
        content_ = value;
      }
    }

    /// <summary>Field number for the "IsKey" field.</summary>
    public const int IsKeyFieldNumber = 2;
    private bool isKey_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool IsKey {
      get { return isKey_; }
      set {
        isKey_ = value;
      }
    }

    /// <summary>Field number for the "ChildNodes" field.</summary>
    public const int ChildNodesFieldNumber = 3;
    private static readonly pbc::MapField<uint, global::TestDotnet.Data.Entities.StrategyNodeEntity>.Codec _map_childNodes_codec
        = new pbc::MapField<uint, global::TestDotnet.Data.Entities.StrategyNodeEntity>.Codec(pb::FieldCodec.ForUInt32(8, 0), pb::FieldCodec.ForMessage(18, global::TestDotnet.Data.Entities.StrategyNodeEntity.Parser), 26);
    private readonly pbc::MapField<uint, global::TestDotnet.Data.Entities.StrategyNodeEntity> childNodes_ = new pbc::MapField<uint, global::TestDotnet.Data.Entities.StrategyNodeEntity>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::MapField<uint, global::TestDotnet.Data.Entities.StrategyNodeEntity> ChildNodes {
      get { return childNodes_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as StrategyNodeEntity);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(StrategyNodeEntity other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Content != other.Content) return false;
      if (IsKey != other.IsKey) return false;
      if (!ChildNodes.Equals(other.ChildNodes)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Content != 0) hash ^= Content.GetHashCode();
      if (IsKey != false) hash ^= IsKey.GetHashCode();
      hash ^= ChildNodes.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Content != 0) {
        output.WriteRawTag(8);
        output.WriteUInt32(Content);
      }
      if (IsKey != false) {
        output.WriteRawTag(16);
        output.WriteBool(IsKey);
      }
      childNodes_.WriteTo(output, _map_childNodes_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Content != 0) {
        output.WriteRawTag(8);
        output.WriteUInt32(Content);
      }
      if (IsKey != false) {
        output.WriteRawTag(16);
        output.WriteBool(IsKey);
      }
      childNodes_.WriteTo(ref output, _map_childNodes_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (Content != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Content);
      }
      if (IsKey != false) {
        size += 1 + 1;
      }
      size += childNodes_.CalculateSize(_map_childNodes_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(StrategyNodeEntity other) {
      if (other == null) {
        return;
      }
      if (other.Content != 0) {
        Content = other.Content;
      }
      if (other.IsKey != false) {
        IsKey = other.IsKey;
      }
      childNodes_.MergeFrom(other.childNodes_);
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Content = input.ReadUInt32();
            break;
          }
          case 16: {
            IsKey = input.ReadBool();
            break;
          }
          case 26: {
            childNodes_.AddEntriesFrom(input, _map_childNodes_codec);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            Content = input.ReadUInt32();
            break;
          }
          case 16: {
            IsKey = input.ReadBool();
            break;
          }
          case 26: {
            childNodes_.AddEntriesFrom(ref input, _map_childNodes_codec);
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
