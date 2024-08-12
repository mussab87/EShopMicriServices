﻿namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
                string Name,
                List<string> Category,
                string Description,
                string ImageFile,
                decimal Price
                ) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateProductCommandValidtor : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidtor()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

public class CreateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //create Product entity from command object
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        //To Do
        //save to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        //return CreateProductResult result
        return new CreateProductResult(product.Id);
    }
}

