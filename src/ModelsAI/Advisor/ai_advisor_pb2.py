# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# NO CHECKED-IN PROTOBUF GENCODE
# source: ai_advisor.proto
# Protobuf Python Version: 5.27.2
"""Generated protocol buffer code."""
from google.protobuf import descriptor as _descriptor
from google.protobuf import descriptor_pool as _descriptor_pool
from google.protobuf import runtime_version as _runtime_version
from google.protobuf import symbol_database as _symbol_database
from google.protobuf.internal import builder as _builder
_runtime_version.ValidateProtobufRuntimeVersion(
    _runtime_version.Domain.PUBLIC,
    5,
    27,
    2,
    '',
    'ai_advisor.proto'
)
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor_pool.Default().AddSerializedFile(b'\n\x10\x61i_advisor.proto\x12\tAiAdvisor\"\xbc\x02\n#GetFinancialAdviceForAccountRequest\x12\x0f\n\x07\x62\x61lance\x18\x01 \x01(\x01\x12P\n\x0ctransactions\x18\x02 \x03(\x0b\x32:.AiAdvisor.GetFinancialAdviceForAccountRequest.Transaction\x12\x42\n\x05goals\x18\x03 \x03(\x0b\x32\x33.AiAdvisor.GetFinancialAdviceForAccountRequest.Goal\x1a/\n\x0bTransaction\x12\x0e\n\x06\x61mount\x18\x01 \x01(\x01\x12\x10\n\x08\x63\x61tegory\x18\x02 \x01(\t\x1a=\n\x04Goal\x12\x10\n\x08\x63\x61tegory\x18\x01 \x01(\t\x12\x14\n\x0c\x61mount_saved\x18\x02 \x01(\x01\x12\r\n\x05total\x18\x03 \x01(\x01\"*\n\x17\x46inancialAdviceResponse\x12\x0f\n\x07message\x18\x01 \x01(\t2u\n\tAiAdvisor\x12h\n\x12GetFinancialAdvice\x12..AiAdvisor.GetFinancialAdviceForAccountRequest\x1a\".AiAdvisor.FinancialAdviceResponseB\x11\xaa\x02\x0eInfrastructureb\x06proto3')

_globals = globals()
_builder.BuildMessageAndEnumDescriptors(DESCRIPTOR, _globals)
_builder.BuildTopDescriptorsAndMessages(DESCRIPTOR, 'ai_advisor_pb2', _globals)
if not _descriptor._USE_C_DESCRIPTORS:
  _globals['DESCRIPTOR']._loaded_options = None
  _globals['DESCRIPTOR']._serialized_options = b'\252\002\016Infrastructure'
  _globals['_GETFINANCIALADVICEFORACCOUNTREQUEST']._serialized_start=32
  _globals['_GETFINANCIALADVICEFORACCOUNTREQUEST']._serialized_end=348
  _globals['_GETFINANCIALADVICEFORACCOUNTREQUEST_TRANSACTION']._serialized_start=238
  _globals['_GETFINANCIALADVICEFORACCOUNTREQUEST_TRANSACTION']._serialized_end=285
  _globals['_GETFINANCIALADVICEFORACCOUNTREQUEST_GOAL']._serialized_start=287
  _globals['_GETFINANCIALADVICEFORACCOUNTREQUEST_GOAL']._serialized_end=348
  _globals['_FINANCIALADVICERESPONSE']._serialized_start=350
  _globals['_FINANCIALADVICERESPONSE']._serialized_end=392
  _globals['_AIADVISOR']._serialized_start=394
  _globals['_AIADVISOR']._serialized_end=511
# @@protoc_insertion_point(module_scope)
