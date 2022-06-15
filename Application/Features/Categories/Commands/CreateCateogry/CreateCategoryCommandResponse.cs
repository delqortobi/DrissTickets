using Application.Responses;

namespace Application.Features.Categories.Commands.CreateCateogry;

public class CreateCategoryCommandResponse: BaseResponce
{
    public CreateCategoryCommandResponse(): base()
    {

    }

    public CreateCategoryDto Category { get; set; }
}