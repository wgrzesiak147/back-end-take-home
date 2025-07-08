# Feature Flag Service

## Context

As our engineering team grows, we need a better way to manage feature releases. We want to be able to deploy new code to production but keep the associated features hidden from users until they are ready. A simple, internal feature flag service would allow us to enable or disable features for different applications without a new deployment.

## Task

Your assignment is to design and implement a microservice for managing feature flags. The service will store the state of various flags across different environments and must keep a log of all changes. Client applications will query the service to check the state of these flags.

### Business Rules

- A feature flag is identified by a unique name.
- A flag's state is environment-specific. It can be active in one environment and inactive in another.
- By default, new flags are created in an inactive state across all environments.
- Every change to a flag's state must be recorded in an audit log to track what changed and when it occurred.
- The service should support gradual rollouts, allowing flags to be active for only a portion of users in an environment.

### User Stories

1. **As a developer,** I want to register a new feature flag in the system so that its rollout can be managed independently of deployments.
2. **As a developer,** I want to retrieve a list of all existing feature flags so that I can get an overview of all managed features.
3. **As a developer,** I want to view the complete configuration of a single flag across all environments at once so that I can understand its current state.
4. **As a developer,** I want to activate or deactivate a feature flag for a specific environment so that I can control its visibility for testing or release.
5. **As a developer,** I want to view the complete history of state changes for a flag so that I can audit when, and by whom, it was modified.
6. **As a developer,** I want to remove a feature flag from the system entirely once the feature has been fully released and the code has been cleaned up.
7. **As an application,** I need to check if a feature flag is currently active for my specific environment so that I can decide whether to expose the feature.
8. **As a product manager,** I want to configure a flag to be active for a certain percentage of users in an environment to allow for a gradual rollout.

### Technical Requirements

- .NET with code in F# or C#.
- RESTful API.
- Data persistence (e.g., files or a database).
- Test framework of your choice, if you plan to write tests.
- Git for change tracking.

## Solution

- Try not to spend more than 4 hours on this task.
- In case you don't implement everything, please document what's left and what you would do next.
- If you use AI, highlight AI-aided parts, including the prompts and models you used.
- For unspecified rules or behaviors, apply common sense and industry best practices.
- Deliver a working solution and include instructions on how to run it.

### Submission

Provide a link to a GitHub repository with your solution to the recruitment team.
