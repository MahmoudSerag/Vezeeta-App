﻿using Application.Dtos;
using Core.Models;

namespace Application.Interfaces.Repositories
{
    public interface IBookingRepository
    {
        public bool CheckDiscountEligibility(string patientId);
        public int GetBookingStatusId(string bookingStatusName);
        public Discount GetDiscountByCodeName(DiscountCodeCouponDto discountCodeCouponDto);
        public AppointmentTime GetAppointmentTimeById(int appointmentTimeId);
        public Appointment GetAppointmentById(int appointmentTimeId);
        public int GetDoctorExaminationPrice(string doctorId);
        public void ConfirmBooking(Booking booking, int statusId);
        public BookingStatus GetBookingStatusById(int bookingStatusId);
        public Booking GetBookingById(int bookingId);
        public string AddNewBooking(
            string patientId,
            int appointmentTimeId,
            DiscountCodeCouponDto discountCodeCouponDto
        );
    }
}
