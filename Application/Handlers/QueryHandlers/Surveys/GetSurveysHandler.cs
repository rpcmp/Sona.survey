using Application.Models.Surveys;
using Application.Queries.Surveys;
using Core.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.QueryHandlers.Surveys
{
    public class GetSurveysHandler : IRequestHandler<GetSurveysQuery, SurveyDto[]>
    {
        private readonly ISurveyRepository _surveyRepository;

        public GetSurveysHandler(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        Task<SurveyDto[]> IRequestHandler<GetSurveysQuery, SurveyDto[]>.Handle(GetSurveysQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
