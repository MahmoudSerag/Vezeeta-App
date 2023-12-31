﻿using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;

namespace Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        public string CreateNewBooking(
            string patientId,
            int appointmentTimeId,
            DiscountCodeCouponDto discountCodeCouponDto
        )
        {
            try
            {
                var isAppointmentBooked = bookingRepository.IsAppointmentTimeBooked(
                    appointmentTimeId
                );

                if (isAppointmentBooked)
                    return "Appointment time is already booked";

                var appointmentTime = bookingRepository.GetAppointmentTimeById(appointmentTimeId);

                if (appointmentTime == null)
                    return "No appointment time with the given Id";

                var result = bookingRepository.AddNewBooking(
                    patientId,
                    appointmentTimeId,
                    discountCodeCouponDto
                );

                this.bookingRepository.UpdateAppointmentTime(appointmentTime);
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ConfirmBooking(string doctorId, int bookingId)
        {
            try
            {
                var booking = this.bookingRepository.GetBookingById(bookingId);

                if (booking == null)
                    return "Booking not found with the given Id";

                var appointment = this.bookingRepository.GetAppointmentById(
                    booking.AppointmentTimeId
                );

                if (appointment == null || appointment.DoctorId != doctorId)
                    return "Forbidden. You can't access this content";

                var bookingStatus = this.bookingRepository.GetBookingStatusById(booking.StatusId);

                if (
                    bookingStatus.Name.ToString() == "Completed"
                    || bookingStatus.Name.ToString() == "Cancelled"
                )
                    return "Forbidden. The booking status has been officially confirmed or cancelled";

                var userBookingTracking = this.bookingRepository.GetUserBookingTracking(
                    booking.PatientId
                );

                if (userBookingTracking == null)
                    this.bookingRepository.AddNewBookingTracking(booking.PatientId);
                else
                    this.bookingRepository.UpdateUserBookingTracking(userBookingTracking);

                var appointmentTime = this.bookingRepository.GetAppointmentTimeById(
                    booking.AppointmentTimeId
                );

                this.bookingRepository.UpdateAppointmentTime(appointmentTime);

                var completedStatusId = this.bookingRepository.GetBookingStatusId("Completed");

                this.bookingRepository.UpdateBookingStatus(booking, completedStatusId);

                return "Succeeded";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string CancelBooking(string doctorId, int bookingId)
        {
            try
            {
                var booking = this.bookingRepository.GetBookingById(bookingId);

                if (booking == null)
                    return "Booking not found with the given Id";

                var appointment = this.bookingRepository.GetAppointmentById(
                    booking.AppointmentTimeId
                );

                if (appointment == null || appointment.DoctorId != doctorId)
                    return "Forbidden. You can't access this content";

                var bookingStatus = this.bookingRepository.GetBookingStatusById(booking.StatusId);

                if (
                    bookingStatus.Name.ToString() == "Completed"
                    || bookingStatus.Name.ToString() == "Cancelled"
                )
                    return "Forbidden. The booking status has been officially confirmed or cancelled";

                var completedStatusId = this.bookingRepository.GetBookingStatusId("Cancelled");

                this.bookingRepository.UpdateBookingStatus(booking, completedStatusId);

                return "Succeeded";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public object GetCountOfBookings()
        {
            try
            {
                List<int> countOfBookings = this.bookingRepository.GetCountOfBookings();

                if (countOfBookings.Count == 0)
                    return "No bookings found.";

                return countOfBookings;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public (object, int) GetAllBookingsForPatient(string patientId, int page, int limit)
        {
            var bookings = this.bookingRepository.GetAllBookingsForPatient(patientId, page, limit);
            int countOfBookings = this.bookingRepository.GetTotalBookingsCount();

            if (bookings == null || countOfBookings == 0)
                return ("No bookings found.", 0);

            return (bookings, countOfBookings);
        }

        public (object, int) GetAllBookingsForDoctor(string doctorId, int page, int limit)
        {
            var bookings = this.bookingRepository.GetAllBookingsForDoctor(doctorId, page, limit);
            int countOfBookings = this.bookingRepository.GetTotalBookingsCount();

            if (bookings == null || countOfBookings == 0)
                return ("No bookings found.", 0);

            return (bookings, countOfBookings);
        }
    }
}
