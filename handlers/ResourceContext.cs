using TrackingCodeApi.services;
using AutoMapper;

namespace TrackingCodeApi.handlers;

public record ResourceContext(
    IMapper Mapper,
    ILinkService LinkService,
    string ParentResourceName,
    string ChildResourceName
);
