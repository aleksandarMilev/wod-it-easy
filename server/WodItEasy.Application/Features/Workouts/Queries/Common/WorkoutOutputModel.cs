﻿namespace WodItEasy.Application.Features.Workouts.Queries.Common
{
    using System;
    using AutoMapper;
    using Domain.Common;
    using Domain.Models.Workouts;
    using Mapping;

    public class WorkoutOutputModel : IMapFrom<Workout>
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string ImageUrl { get; set; } = default!;

        public int MaxParticipantsCount { get; set; }

        public int CurrentParticipantsCount { get; set; }

        public DateTime StartsAt { get; set; }

        public string Type { get; set; } = default!;

        public virtual void Mapping(Profile mapper)
            => mapper
                .CreateMap<Workout, WorkoutOutputModel>()
                .ForMember(
                    dest => dest.Type,
                    opt => opt.MapFrom(
                        src => Enumeration.NameFromValue<WorkoutType>(
                            src.Type != null
                                ? src.Type.Value
                                : WorkoutType.CrossFit.Value)));
    }
}
