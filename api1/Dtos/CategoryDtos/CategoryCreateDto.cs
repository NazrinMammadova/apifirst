using FluentValidation;

namespace api1.Dtos.CategoryDtos
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
    }
    public class CategoryCreateDtoValidator:AbstractValidator<CategoryCreateDto> 
    { 
        public CategoryCreateDtoValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("bosh qoyma")
                .MaximumLength(100)
                .WithMessage("100den yuxari ola bilmez");
        }
    }
}
