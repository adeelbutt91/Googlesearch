FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
RUN apt-get update && apt-get install -y wget gnupg unzip \
    && wget -q https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb \
    && apt-get install -y ./google-chrome-stable_current_amd64.deb \
    && rm google-chrome-stable_current_amd64.deb
RUN DRIVER_VERSION=$(wget -qO- "https://chromedriver.storage.googleapis.com/LATEST_RELEASE") \
    && wget -q "https://chromedriver.storage.googleapis.com/${DRIVER_VERSION}/chromedriver_linux64.zip" \
    && unzip chromedriver_linux64.zip -d /usr/local/bin \
    && rm chromedriver_linux64.zip \
    && chmod +x /usr/local/bin/chromedriver
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet build --configuration Release
RUN dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI
RUN mkdir -p /reports
ENV CI=true
ENTRYPOINT dotnet test --configuration Release --no-build \
  && livingdoc test-assembly bin/Release/net8.0/Googlesearch.dll \
       -t bin/Release/net8.0/TestExecution.json \
       -o /reports/LivingDoc.html
