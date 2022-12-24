using Application.Models.Surveys;
using MediatR;

namespace Application.Queries.Surveys
{
    public class GetSurveysQuery : IRequest<SurveyDto[]>
    {
    }
}
