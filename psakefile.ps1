properties {
    $runSonarScanner = $true
    $project_key = "bowlinggame"
    $sonarqube_host_url = $env:SonarCloud_URL
    $sonarqube_login = $env:SonarCloud_Token
    $sonarqube_organisation = $env:SonarCloud_Organisation
}

task default -depend TestProperties, UnitTests

task TestProperties {
    Assert($project_key -ne $null) "project_key should not be null"
    Assert($sonarqube_host_url -ne $null) "sonarqube_host_url should not be null"
    Assert($sonarqube_login -ne $null) "sonarqube_login should not be null"
    Assert($sonarqube_organisation -ne $null) "sonarqube_organisation should not be null"
}

task Clean {
    exec {
        dotnet clean ./BowlingGame.sln
    }
}

task Build -depend Clean {
    if ($runSonarScanner) {
        exec {
            dotnet-sonarscanner begin /k:"$project_key" /d:sonar.host.url="$sonarqube_host_url" /d:sonar.login="$sonarqube_login" /d:sonar.organization="$sonarqube_organisation" /d:sonar.cs.opencover.reportsPaths="**\coverage.opencover.xml" /d:sonar.coverage.exclusions="**Tests*.cs"
        }    
    }

    exec {
        dotnet build ./BowlingGame.sln
    }
}

task UnitTests -depend Build {
    exec {
        dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover ./BowlingGame.Tests/BowlingGame.Tests.csproj
    }

    if ($runSonarScanner) {
        exec {
            dotnet-sonarscanner end /d:sonar.login="$sonarqube_login"
        }    
    }
}
