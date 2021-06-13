
SELECT HAREKETID,URUNAD ,TBL_FIRMAHAREKETLER.ADET,
(TBL_PERSONELLER.AD+' '+SOYAD) AS ' AD SOYAD',TBL_FIRMALAR.AD
,FIYAT,TOPLAM,FATURAID,TARIH,NOTLAR
FROM TBL_FIRMAHAREKETLER
INNER JOIN 
TBL_URUNLER
ON
[URUNID]=TBL_URUNLER.ID
INNER JOIN 
TBL_PERSONELLER
on
TBL_FIRMAHAREKETLER.PERSONEL=TBL_PERSONELLER.ID
INNER JOIN
TBL_FIRMALAR
ON
TBL_FIRMAHAREKETLER.FIRMA=TBL_FIRMALAR.ID