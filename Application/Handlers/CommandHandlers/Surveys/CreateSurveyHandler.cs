using Application.Commands.Surveys;
using Core.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers.Surveys
{
    internal class CreateSurveyHandler : IRequestHandler<CreateSurveyCommand>
    {
        private readonly ISurveyRepository _surveyRepository;

        public CreateSurveyHandler(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public Task<Unit> Handle(CreateSurveyCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
