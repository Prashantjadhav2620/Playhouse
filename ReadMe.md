TurfMate/
┣ TurfMate.Api/              # ASP.NET MVC Web API backend
┃ ┣ Controllers/
┃ ┃ ┣ AuthController.cs
┃ ┃ ┣ TurfController.cs
┃ ┃ ┣ BookingController.cs
┃ ┣ Models/
┃ ┃ ┣ User.cs
┃ ┃ ┣ Turf.cs
┃ ┃ ┣ Booking.cs
┃ ┃ ┣ DTOs/                  # Add DTOs for TypeScript compatibility
┃ ┃ ┃ ┣ UserDto.cs
┃ ┃ ┃ ┣ TurfDto.cs
┃ ┃ ┃ ┣ BookingDto.cs
┃ ┃ ┃ ┣ AuthDto.cs
┃ ┣ Repositories/
┃ ┃ ┣ UserRepository.cs
┃ ┃ ┣ TurfRepository.cs
┃ ┃ ┣ BookingRepository.cs
┃ ┣ Services/
┃ ┃ ┣ AuthService.cs
┃ ┃ ┣ TurfService.cs
┃ ┃ ┣ BookingService.cs
┃ ┣ TurfMate.Api.csproj
┃ ┗ Web.config

┣ turfmate-client/           # React + TypeScript frontend
┃ ┣ src/
┃ ┃ ┣ components/
┃ ┃ ┃ ┣ auth/
┃ ┃ ┃ ┃ ┣ Login.tsx
┃ ┃ ┃ ┃ ┣ Register.tsx
┃ ┃ ┃ ┣ turf/
┃ ┃ ┃ ┃ ┣ TurfList.tsx
┃ ┃ ┃ ┃ ┣ TurfCard.tsx
┃ ┃ ┃ ┃ ┣ TurfDetails.tsx
┃ ┃ ┃ ┣ booking/
┃ ┃ ┃ ┃ ┣ TurfBooking.tsx
┃ ┃ ┃ ┃ ┣ BookingForm.tsx
┃ ┃ ┃ ┃ ┣ BookingList.tsx
┃ ┃ ┃ ┣ admin/
┃ ┃ ┃ ┃ ┣ AdminDashboard.tsx
┃ ┃ ┃ ┃ ┣ UserManagement.tsx
┃ ┃ ┃ ┃ ┣ TurfManagement.tsx
┃ ┃ ┃ ┣ common/
┃ ┃ ┃ ┃ ┣ Header.tsx
┃ ┃ ┃ ┃ ┣ Footer.tsx
┃ ┃ ┃ ┃ ┣ LoadingSpinner.tsx
┃ ┃ ┃ ┃ ┣ Modal.tsx
┃ ┃ ┣ types/
┃ ┃ ┃ ┣ user.types.ts
┃ ┃ ┃ ┣ turf.types.ts
┃ ┃ ┃ ┣ booking.types.ts
┃ ┃ ┃ ┣ api.types.ts
┃ ┃ ┣ services/
┃ ┃ ┃ ┣ api.ts
┃ ┃ ┃ ┣ auth.service.ts
┃ ┃ ┃ ┣ turf.service.ts
┃ ┃ ┃ ┣ booking.service.ts
┃ ┃ ┣ hooks/
┃ ┃ ┃ ┣ useAuth.ts
┃ ┃ ┃ ┣ useTurf.ts
┃ ┃ ┃ ┣ useBooking.ts
┃ ┃ ┣ contexts/
┃ ┃ ┃ ┣ AuthContext.tsx
┃ ┃ ┣ utils/
┃ ┃ ┃ ┣ constants.ts
┃ ┃ ┃ ┣ helpers.ts
┃ ┃ ┃ ┣ validation.ts
┃ ┃ ┣ pages/
┃ ┃ ┃ ┣ Home.tsx
┃ ┃ ┃ ┣ Dashboard.tsx
┃ ┃ ┃ ┣ Turfs.tsx
┃ ┃ ┃ ┣ Bookings.tsx
┃ ┃ ┣ styles/
┃ ┃ ┃ ┣ globals.css
┃ ┃ ┃ ┣ components.css
┃ ┃ ┣ App.tsx
┃ ┃ ┣ main.tsx
┃ ┃ ┣ vite-env.d.ts
┃ ┣ public/
┃ ┃ ┣ vite.svg
┃ ┣ package.json
┃ ┣ tsconfig.json
┃ ┣ vite.config.ts
┃ ┣ tailwind.config.js       # If using Tailwind CSS
┃ ┗ README.md

┗ README.md                 # Project documentation