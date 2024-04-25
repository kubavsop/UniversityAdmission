using Admission.Application.Common.Extensions;
using Admission.Dictionary.Application.Context;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Faculty;
using MediatR;

namespace Admission.Dictionary.Application.RpcHandlers;

    public sealed class GetFacultyRequestHandler: IRequestHandler<GetFacultyRequest, IRpcResponse?>
    {
        private readonly IDictionaryDbContext _context;

        public GetFacultyRequestHandler(IDictionaryDbContext context)
        {
            _context = context;
        }

        public async Task<IRpcResponse?> Handle(GetFacultyRequest request, CancellationToken cancellationToken)
        {
            var faculty = await _context.Faculties.GetByIdAsync(request.Id);
            if (faculty == null) return null;

            return new FacultyResponse
            {
                Id = faculty.Id,
                Name = faculty.Name
            };
        }
    }