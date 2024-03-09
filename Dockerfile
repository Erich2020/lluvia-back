
FROM tempolluvia-ssl

COPY . ./home

RUN cd /home && dotnet build TempporalWS.sln -o ../app

CMD ["dotnet", "./app/TempporalWS.dll"]
