FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /app
 
# copy csproj and restore as distinct layers
COPY Checkout.API/Checkout.API.csproj Checkout.API/
COPY Resources/Resources.csproj Resources/
RUN dotnet restore Checkout.API/Checkout.API.csproj
 
# copy everything else and build
COPY . .
WORKDIR /app/Checkout.API
RUN dotnet publish Checkout.API.csproj -c Release -o /out
 
# build runtime image
FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /out ./
 
ENTRYPOINT ["dotnet", "Checkout.API.dll"]