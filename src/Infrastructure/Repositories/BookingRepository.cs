﻿using Application.Dtos;
using Application.Interfaces.Repositories;
using Core.Enums;
using Core.Models;
using Infrastructure.Database.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public BookingRepository(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager
        )
        {
            this.userManager = userManager;
            this.context = context;
        }

        public bool CheckDiscountEligibility(string patientId)
        {
            var userBookingTracking = context.UserBookingTrackings
                .Where(bookTrack => bookTrack.PatientId == patientId)
                .FirstOrDefault();

            if (userBookingTracking != null && userBookingTracking.ApprovedBookingCount == 5)
                return true;

            return false;
        }

        public Discount GetDiscountByCodeName(DiscountCodeCouponDto discountCodeCouponDto)
        {
            var discount = this.context.Discounts
                .Where(dis => dis.DiscountCode == discountCodeCouponDto.DiscountCodeCoupon)
                .FirstOrDefault();

            return discount;
        }

        public int GetBookingStatusId(string bookingStatusName)
        {
            return context.BookingStatus
                .Where(
                    bookingStatus =>
                        bookingStatus.Name == Enum.Parse<BookingStatusEnum>(bookingStatusName)
                )
                .Select(bookingStatus => bookingStatus.Id)
                .FirstOrDefault();
        }

        public AppointmentTime GetAppointmentTimeById(int appointmentTimeId)
        {
            var appointmentTime = context.AppointmentTimes
                .Where(appointmentTime => appointmentTime.Id == appointmentTimeId)
                .FirstOrDefault();

            return appointmentTime;
        }

        public Appointment GetAppointmentById(int appointmentTimeId)
        {
            var appointmentTime = this.GetAppointmentTimeById(appointmentTimeId);

            var appointment = context.Appointments
                .Where(appointment => appointment.Id == appointmentTime.AppointmentId)
                .FirstOrDefault();

            return appointment;
        }

        public int GetDoctorExaminationPrice(string doctorId)
        {
            return this.context.ExaminationPrices
                .Where(ex => ex.DoctorId == doctorId)
                .Select(ex => ex.Price)
                .FirstOrDefault();
        }

        public DiscountType GetDiscountType(int discountTypeId)
        {
            var discountType = this.context.DiscountTypes
                .Where(dis => dis.Id == discountTypeId)
                .FirstOrDefault();

            return discountType;
        }

        public bool IsAppointmentTimeBooked(int appointmentTimeId)
        {
            var booking = this.context.Bookings
                .Where(booking => booking.AppointmentTimeId == appointmentTimeId)
                .FirstOrDefault();

            if (booking == null)
                return false;

            return true;
        }

        public string AddNewBooking(
            string patientId,
            int appointmentTimeId,
            DiscountCodeCouponDto discountCodeCouponDto
        )
        {
            try
            {
                int statusId = this.GetBookingStatusId("Pending");
                bool isDiscountEligible = this.CheckDiscountEligibility(patientId);
                var appointment = this.GetAppointmentById(appointmentTimeId);
                var discount = this.GetDiscountByCodeName(discountCodeCouponDto);
                int doctorExaminationPrice = this.GetDoctorExaminationPrice(appointment.DoctorId);

                if (discountCodeCouponDto.DiscountCodeCoupon != null)
                {
                    if (discount != null)
                    {
                        if (isDiscountEligible)
                        {
                            int finalPrice;
                            int discountId = discount.Id;

                            var discountType = this.GetDiscountType(discount.DiscountTypeId);

                            if (discountType != null && discountType.Name == "Percentage")
                                finalPrice =
                                    doctorExaminationPrice
                                    - (doctorExaminationPrice * discount.DiscountValue / 100);
                            else
                            {
                                if (discount.DiscountValue >= doctorExaminationPrice)
                                    finalPrice = 0;
                                else
                                    finalPrice = doctorExaminationPrice - discount.DiscountValue;
                            }

                            this.context.Bookings.Add(
                                new Booking
                                {
                                    StatusId = statusId,
                                    AppointmentTimeId = appointmentTimeId,
                                    PatientId = patientId,
                                    DiscountId = discountId,
                                    Date = appointment.Date,
                                    CreatedAt = DateTime.Now,
                                    Price = doctorExaminationPrice,
                                    FinalPrice = finalPrice,
                                }
                            );

                            var userBookingTracking = this.GetUserBookingTracking(patientId);
                            userBookingTracking.ApprovedBookingCount = 0;
                        }
                        else
                        {
                            this.context.Bookings.Add(
                                new Booking
                                {
                                    StatusId = statusId,
                                    AppointmentTimeId = appointmentTimeId,
                                    PatientId = patientId,
                                    Date = appointment.Date,
                                    CreatedAt = DateTime.Now,
                                    Price = doctorExaminationPrice,
                                    FinalPrice = doctorExaminationPrice,
                                }
                            );
                        }
                    }
                    else
                    {
                        return "Discount code not found";
                    }
                }
                else
                {
                    this.context.Bookings.Add(
                        new Booking
                        {
                            StatusId = statusId,
                            AppointmentTimeId = appointmentTimeId,
                            PatientId = patientId,
                            Date = appointment.Date,
                            CreatedAt = DateTime.Now,
                            Price = doctorExaminationPrice,
                            FinalPrice = doctorExaminationPrice,
                        }
                    );
                }

                this.context.SaveChanges();
                return "Succeeded";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Booking GetBookingById(int bookingId)
        {
            var booking = this.context.Bookings
                .Where(booking => booking.Id == bookingId)
                .FirstOrDefault();

            return booking;
        }

        public BookingStatus GetBookingStatusById(int bookingStatusId)
        {
            var bookingStatus = this.context.BookingStatus
                .Where(booking => booking.Id == bookingStatusId)
                .FirstOrDefault();

            return bookingStatus;
        }

        public UserBookingTracking GetUserBookingTracking(string patientId)
        {
            var userBookingTracking = this.context.UserBookingTrackings
                .Where(userBooking => userBooking.PatientId == patientId)
                .FirstOrDefault();

            return userBookingTracking;
        }

        public void UpdateUserBookingTracking(UserBookingTracking userBookingTracking)
        {
            userBookingTracking.ApprovedBookingCount += 1;
            this.context.SaveChanges();
        }

        public void AddNewBookingTracking(string patientId)
        {
            var userBookingTracking = new UserBookingTracking
            {
                PatientId = patientId,
                ApprovedBookingCount = 1
            };
            this.context.UserBookingTrackings.Add(userBookingTracking);
            this.context.SaveChanges();
        }

        public void UpdateAppointmentTime(AppointmentTime appointmentTime)
        {
            appointmentTime.IsBooked = true;
            this.context.SaveChanges();
        }

        public void UpdateBookingStatus(Booking booking, int statusId)
        {
            booking.StatusId = statusId;
            this.context.SaveChanges();
        }

        public List<int> GetCountOfBookings()
        {
            int pendingRequests = 0;
            int completedRequests = 0;
            int cancelledRequests = 0;
            List<int> countOfBookings = new List<int>();

            var bookings = this.context.Bookings;

            foreach (var booking in bookings)
            {
                if (booking.StatusId == 1)
                    pendingRequests++;
                else if (booking.StatusId == 2)
                    completedRequests++;
                else
                    cancelledRequests++;
            }

            countOfBookings.Add(pendingRequests);
            countOfBookings.Add(completedRequests);
            countOfBookings.Add(cancelledRequests);

            return countOfBookings;
        }

        public object GetAllBookingsForPatient(string patientId, int page, int limit)
        {
            var bookings = this.context.Bookings
                .Where(booking => booking.PatientId == patientId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .Include(booking => booking.AppointmentTime)
                .ThenInclude(appointmentTime => appointmentTime.Appointment)
                .ThenInclude(appointment => appointment.Doctor)
                .ThenInclude(doctor => doctor.Specialization)
                .Include(booking => booking.AppointmentTime)
                .ThenInclude(appointmentTime => appointmentTime.Appointment)
                .ThenInclude(appointment => appointment.Day)
                .Include(booking => booking.AppointmentTime)
                .ThenInclude(appointmentTime => appointmentTime.Time)
                .Include(booking => booking.BookingStatus)
                .Include(booking => booking.Discount)
                .Select(
                    booking =>
                        new
                        {
                            doctorName = $"{booking.AppointmentTime.Appointment.Doctor.FirstName} {booking.AppointmentTime.Appointment.Doctor.LastName}",
                            image = booking.AppointmentTime.Appointment.Doctor.Image,
                            specialization = booking.AppointmentTime.Appointment.Doctor.Specialization.Title.ToString(),
                            day = booking.AppointmentTime.Appointment.Day.Name.ToString(),
                            time = booking.AppointmentTime.Time.TimeValue,
                            booking.Price,
                            booking.FinalPrice,
                            statusName = booking.BookingStatus.Name.ToString(),
                            discountCode = booking.Discount.DiscountCode
                        }
                );

            return bookings;
        }

        public object GetAllBookingsForDoctor(string doctorId, int page, int limit)
        {
            var bookings = this.context.Appointments
                .Where(appointment => appointment.DoctorId == doctorId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .Include(appointment => appointment.AppointmentTimes)
                .ThenInclude(appointmentTimes => appointmentTimes.Booking)
                .ThenInclude(booking => booking.Patient);

            return bookings;
        }

        public int GetTotalBookingsCount()
        {
            return this.context.Bookings.Count();
        }
    }
}
