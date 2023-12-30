# Get the last write time of ChangeLog.md
$lastModified = (Get-Item -Path ChangeLog.md).LastWriteTime

# Format the date for git log
$lastModifiedDate = $lastModified.ToString("yyyy-MM-dd HH:mm:ss")

# Create a temporary log file with commits since the last modified date of ChangeLog.md
git log --since="$lastModifiedDate" --pretty=format:"%ad - %s [%an]" --date=format:'%Y-%m-%d' > templog.txt

# Read the existing content of ChangeLog.md
$existingContent = Get-Content -Path ChangeLog.md

# Read the temporary log file
$newContent = Get-Content -Path templog.txt

# Prepare formatted content for ChangeLog.md
$formattedNewContent = "# Change Log`n`n## [Recent Changes]`n`n"

# Append each new line from the log to the formatted content
foreach ($line in $newContent) {
    $formattedNewContent += "- " + $line + "`n"
}

# Combine new and existing contents
$combinedContent = $formattedNewContent + "`n`n" + $existingContent

# Write the combined content to ChangeLog.md
$combinedContent | Out-File -FilePath ChangeLog.md

# Clean up the temporary log file
Remove-Item -Path templog.txt

# Add, commit, and push changes
git add ChangeLog.md
git commit -m "Updated ChangeLog with recent changes"
git push
