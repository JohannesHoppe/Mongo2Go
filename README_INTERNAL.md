# Mongo2Go - Knowledge for Maintainers

## Creating a Release

Mongo2Go uses [MinVer](https://github.com/adamralph/minver) for versioning.
Releases are fully automated via GitHub Actions and triggered by tagging a commit with the desired semantic version number.
This process involves two steps to ensure reliable deployments.

### Steps to Create a Release

1. **Push Your Changes**
   - Commit and push your changes to the main branch. This will trigger a CI build to validate the changes.
     ```bash
     git commit -m "Your commit message"
     git push
     ```

2. **Wait for the CI Build**
   - Ensure that the GitHub Actions workflow completes successfully. This confirms your changes are valid.

3. **Tag the Commit**
   - Once the CI build passes, create a lightweight tag with the desired version number:
     ```bash
     git tag v4.0.0
     ```
   - Push the tag to trigger the deployment workflow:
     ```bash
     git push --tags
     ```

4. **Draft Release Created**
   - The workflow will:
     1. Create a multi-target NuGet package.
     2. Publish the package to nuget.org.
     3. Create a **draft release** on GitHub with a placeholder note.

5. **Review and Finalize the Release**
   - Visit the [Releases page](https://github.com/Mongo2Go/Mongo2Go/releases).
   - Open the draft release, update the release notes with details about the changes (e.g., changelog, features, fixes), and publish the release manually.


## Workflow Details

- **Two-Step Process**:
  1. The first push (commit) triggers a CI build to validate the changes.
  2. The second push (tag) triggers the deployment workflow.

- **Triggers**:
  - Commits are validated for all branches.
  - Tags starting with `v` trigger deployment.

- **Draft Releases**:
  - Releases are created as drafts, allowing maintainers to review and add release notes before publishing.

- **Automation**:
  - The workflow automates building, testing, publishing to nuget.org, and creating a draft GitHub release.


## Best Practices for Maintainers

- **Semantic Versioning**: Ensure that tags follow the [semantic versioning](https://semver.org/) format (`vMAJOR.MINOR.PATCH`).
- **Pre-Releases**: Use pre-release tags for non-final versions (e.g., `v4.0.0-rc.1`).
- **Detailed Release Notes**: Always add detailed information to the GitHub release, highlighting major changes, fixes, and improvements.
- **Final Review**: Review the draft release to ensure all details are correct before publishing.

