using FluentValidation;
using HH.BucketList.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HH.BucketList.Domain.Validators
{
    public class BucketValidator: AbstractValidator<BucketL>
    {
        public BucketValidator()
        {
            RuleFor(bucketL => bucketL.Title)
                .NotEmpty()
                .WithMessage("Title cannot be empty")
                .Length(2, 20)
                .WithMessage("Length must be between 2 and 20");

            RuleFor(bucketL => bucketL.Description)
                .NotEqual(b => b.Title)
                .WithMessage("Description must be different from Title")
                .NotEmpty()
                .WithMessage("Description cannot be empty");            
        }
    }
}
