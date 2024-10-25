# Images Generator: Automated Header Image Generation

- **Project Type**: Product (Web Application)
- **Platform**: Web application
- **Integrations**:
  - Stripe (payment processing)
  - RG-SVC-ImagesGenerator (image generation based on user data)
- **Project Complexity**: Low
- **Technologies**: 
  - Primary: .NET
- **Languages**: English

## Description

**Images Generator** is a web service designed to create header images based on user-entered data. The service uses machine learning technologies to generate unique images that meet client specifications.

## Key Features

- **Accent Color Selection**: Users can select a primary color for the image to match their brand or preference.
- **Subject Area Selection**: A dropdown menu allows users to specify the image's theme (e.g., "perfumery" or "beauty").
- **Additional Details**: Users can enter additional details in an input field, which are taken into account during image generation.

## Monetization Model

Users pay for the service upfront, 99 cents for 64 images. It's worth considering offering two pricing options: 99 cents and 49 cents.

## Target Audience

Entrepreneurs, marketers, designers, and any other professionals or hobbyists who need unique images for marketing campaigns, social media, articles, landing pages, and other purposes.

## Supporting Materials

- [User Workflow](./docs/user-workflow.md)
- [Subject Areas](./docs/subject-areas.md)

## Development Cycle

1. Home Page
2. Parameter Selection
3. Payment Page
4. Image Generation
5. Results Page

## Development Iterations

1. [Proof of Concept](./docs/iteration-1.md) `Dev-done`
2. [Image Generation Microservice](./docs/iteration-2.md)
3. [Stripe Integration](./docs/iteration-3.md)

## Ideas and Notes

- Defining a more specific target audience could help create a more focused and effective product.
- Add a feature for sharing projects on social media. Ask users if they achieved their desired result, and if so, encourage them to share it on social media.
- Include information about the development team, their experience, and their motivation for creating this project. This can help build trust and a personal connection with your users.

## Instructions and Documentation

- [Setting Up .NET Razor Pages, VueJs, and WebPack](./docs/how-to-setup-project/readme.md)
- [How to Add Bootstrap 5 to the Project](./docs/how-to-add-bootstrap.md)
