# FROM mcr.microsoft.com/dotnet/sdk:5.0 as builder
FROM mcr.microsoft.com/dotnet/sdk@sha256:92a5929ec1f6fde83e3a5df6b5bf83bb0761b70ab22d338d3efdf8b725658904 as builder
WORKDIR /build/plugin
COPY plugin/*.csproj /build/plugin/
COPY references/ /build/references/
RUN dotnet restore
COPY plugin/source/ /build/plugin/source/
RUN dotnet build --configuration Release --output /output

FROM alpine:latest
COPY --from=builder /output/*.dll /nethermind/plugins/
COPY --from=builder /output/*.pdb /nethermind/plugins/
