FROM microsoft/dotnet:2.0-sdk as builder
LABEL author=cpark@basistech.com

RUN mkdir -p /workspace/source
WORKDIR /scripts
COPY examples.csproj .
COPY docker-entrypoint.sh .
RUN chmod 0755 docker-entrypoint.sh

VOLUME ["/source"]

CMD [ "./docker-entrypoint.sh" ]