# Documentation for AAS Factory Conversion

## Documentation

- [Architecture](./architecture.md)
- [ADT twin naming per AAS](./design/model-data-aas-naming.md)
- [Model data contracts](./design/model-data-contracts.md)
- [Model data raw to AAS](./design/model-data-raw-to-aas.md)
- [Structured Logging](./development/Logging.md)
- [Chaos testing](./development/chaos-testing.md)
- [Terraform infrastructure as code (IaC)](./infra/infrastructure-setup.md)
- [Required developer permissions](./infra/dev-permissions.md)
- [Azure Monitor alerts](./infra/monitoring-alerts.md)

## Documentation Guidance

### Markdown Format

- Documentation should be written in Markdown format.
- Documentation written in Markdown is like any other source code. It follows the same review rules and validation as code.
- We will use `markdownlint` to verify syntax and enforce rules that make text more readable.

### Guidelines

- Organize documents by topic rather than type, this makes it easier to find the documentation.
- Each folder should have a top-level README.md and any other documents within that folder should link directly or indirectly from that README.md
- Document names with more than one word should use dashes instead of spaces, for example `aas-adt-design.md`.
  The same applies to images.
- Avoid duplication of content, instead link to the `single source of truth`.
- Place all images in the root of a separate directory named `images`.
- Avoid writing lines that are over 150 characters long.

### Linting

Linting is important to project consistency and reducing technical debt.
