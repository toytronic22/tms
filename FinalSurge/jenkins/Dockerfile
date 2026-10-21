FROM jenkins/jenkins:lts-jdk21

USER root

RUN apt-get update \
    && apt-get install -y --no-install-recommends curl ca-certificates libicu-dev \
    && rm -rf /var/lib/apt/lists/*

RUN curl -sSL https://dot.net/v1/dotnet-install.sh -o /tmp/dotnet-install.sh \
    && bash /tmp/dotnet-install.sh --channel 10.0 --install-dir /usr/share/dotnet \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    && rm /tmp/dotnet-install.sh

RUN git config --system --add safe.directory '*'

ENV DOTNET_ROOT=/usr/share/dotnet
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1

USER jenkins

RUN jenkins-plugin-cli --plugins git workflow-aggregator pipeline-stage-view credentials-binding allure-jenkins-plugin

COPY --chown=jenkins:jenkins ref/ /usr/share/jenkins/ref/
