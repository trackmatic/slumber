Framework "4.5.2x64"

properties {
	$base_dir = $psake.build_script_dir
    $nspec_runner = "$base_dir\packages\nspec.1.0.1\tools\NSpecRunner.exe"
    $destination_dir = $output
    $nuget = "$base_dir\.nuget\NuGet.exe"
    $nuget_source = "https://api.nuget.org/v3/index.json"
}

Task default -depends Clean, Restore, Build, Test

Task Test {
    # NSpec Tests
    exec { & $nspec_runner "$base_dir\Slumber.Spec\bin\Debug\Slumber.Spec.dll" }
}

Task Publish-Nuget-Package {
	$spec = [xml](get-content "$base_dir/Slumber/Slumber.nuspec")
	$version = $spec.package.metadata.version
	$package = "$base_dir/Slumber.$version.nupkg"
    $package_dir = "$base_dir/Slumber/bin/package"
	$source = $nuget_source
    
    # Prepare package folder
    remove-item $package_dir -R -ErrorAction SilentlyContinue
    new-item -itemtype directory "$package_dir"
    new-item -itemtype directory "$package_dir/lib"
    new-item -itemtype directory "$package_dir/lib/net45"
    
    # Copy nuspec file to package folder
    copy-item "$base_dir/Slumber/Slumber.nuspec" "$package_dir/Slumber.nuspec"

    # Copy libraries to package folders
    get-childitem "$base_dir/Slumber/bin/Debug" | ? { $_.Name -like "Slumber*.dll" -and $_.Name } | % { copy-item $_.FullName "$package_dir/lib/net45" }

    # Create nuget package and upload to nuget
	exec { & $nuget pack "$package_dir/Slumber.nuspec" }
    $apikey = Read-Host -Prompt 'Enter Api Key'
    exec { & $nuget setApiKey $apikey -Source $source}
	exec { & $nuget push "$package" -Source $source }

    # Perform some cleanup on the folder
	remove-item $package
    remove-item $package_dir -R -ErrorAction SilentlyContinue
}

Task Build {

    exec { msbuild "$base_dir/Slumber.sln" }
}

Task Clean {
    Get-ChildItem ./ -include bin,obj -Recurse -Force | % { 
        write-host "Cleaning $_"
        Remove-Item $_ -Recurse -Force  
    }
}

Task Restore {
    Get-ChildItem $base_dir -Filter *.sln | % {
        exec { & "$base_dir\.nuget\NuGet.exe" restore "$_"  }
    }
}