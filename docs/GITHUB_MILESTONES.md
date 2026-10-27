# Version-control and upload checklist

This local project package is prepared first. Do not push it to a remote repository until the owner is ready to review and upload the local source.

Before the future GitHub upload:
1. Review the local source and remove any machine-specific files, private documents, database files and secrets.
2. Initialize Git in the project root if needed and review `git status`.
3. Make meaningful commits tied to real source changes; do not create empty or misleading commits to meet a numeric requirement.
4. Create or select the intended repository and confirm the remote URL before pushing.
5. Run restore, build, tests and LocalDB migration locally, then record genuine results.
6. Upload a clean source ZIP and technical report only after local verification.

The repository already has an earlier CI-generated source snapshot from the previous working session, but no new remote updates were made after the request to work locally first. When ready, compare this local folder with the intended remote before deciding how to sync it.
