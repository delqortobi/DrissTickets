using Application.Responses;

namespace Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandResponse:BaseResponce
{
    public CreateCategoryCommandResponse(): base()
    {
        
    }
    
    public  CreateCategoryDto Category { get; set; }
}