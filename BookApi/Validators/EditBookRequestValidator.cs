using BookApi.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Validators
{
    public class EditBookRequestValidator : AbstractValidator<Book>
    {
        public EditBookRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
             RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(request => request.Name)
                .NotNull().WithMessage("Name is required")
                .NotEmpty().WithMessage("Name should not be empty")
                .Matches("[a-zA-Z]+").WithMessage("Name not contain any special characters or numbers in it");
            RuleFor(request => request.Author)
                .NotNull().WithMessage("Author is required")
                .NotEmpty().WithMessage("Author should not be empty")
                .Matches("[a-zA-Z]+").WithMessage("Name not contain any special characters or numbers in it");
            RuleFor(request => request.Genre)
                .NotNull().WithMessage("Genre is required")
                .NotEmpty().WithMessage("Genre should not be empty")
                .Must(genre => IsGenreValid(genre)).WithMessage("Genre should be either Adventure, Romance or Horror.");
        }

        private bool IsGenreValid(string genre)
        {
            string[] allowedGenre = { "Horror", "Adventure", "Romance" };
            return allowedGenre.Contains(genre);
        }
    }
}
