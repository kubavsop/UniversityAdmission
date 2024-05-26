using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.DictionaryService.GetDocumentTypes;

public class GetDocumentTypesRequest: AuthorizedRequest, IRpcRequest<DocumentTypesResponse>;