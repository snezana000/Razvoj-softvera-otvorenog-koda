ALTER TABLE [dbo].[Ugovor]  WITH CHECK ADD  CONSTRAINT [FK_Ugovor_Destinacija] FOREIGN KEY([ID_PocetnaDestinacija])
REFERENCES [dbo].[Destinacija] ([ID])
GO
ALTER TABLE [dbo].[Ugovor] CHECK CONSTRAINT [FK_Ugovor_Destinacija]
GO
/****** Object:  ForeignKey [FK_Ugovor_Destinacija1]    Script Date: 09/10/2022 16:16:46 ******/
ALTER TABLE [dbo].[Ugovor]  WITH CHECK ADD  CONSTRAINT [FK_Ugovor_Destinacija1] FOREIGN KEY([ID_KrajnaDestinacija])
REFERENCES [dbo].[Destinacija] ([ID])
GO
ALTER TABLE [dbo].[Ugovor] CHECK CONSTRAINT [FK_Ugovor_Destinacija1]
GO
/****** Object:  ForeignKey [FK_Ugovor_Klijent]    Script Date: 09/10/2022 16:16:46 ******/
ALTER TABLE [dbo].[Ugovor]  WITH CHECK ADD  CONSTRAINT [FK_Ugovor_Klijent] FOREIGN KEY([ID_klijent])
REFERENCES [dbo].[Klijent] ([ID])
GO
ALTER TABLE [dbo].[Ugovor] CHECK CONSTRAINT [FK_Ugovor_Klijent]
GO
/****** Object:  ForeignKey [FK_Ugovor_Paket]    Script Date: 09/10/2022 16:16:46 ******/
ALTER TABLE [dbo].[Ugovor]  WITH CHECK ADD  CONSTRAINT [FK_Ugovor_Paket] FOREIGN KEY([ID_paketa])
REFERENCES [dbo].[Paket] ([ID])
GO
ALTER TABLE [dbo].[Ugovor] CHECK CONSTRAINT [FK_Ugovor_Paket]
GO
/****** Object:  ForeignKey [FK_Hotel_Destinacija]    Script Date: 09/10/2022 16:16:46 ******/
ALTER TABLE [dbo].[Hotel]  WITH CHECK ADD  CONSTRAINT [FK_Hotel_Destinacija] FOREIGN KEY([ID_destinacije])
REFERENCES [dbo].[Destinacija] ([ID])
GO
ALTER TABLE [dbo].[Hotel] CHECK CONSTRAINT [FK_Hotel_Destinacija]
GO
